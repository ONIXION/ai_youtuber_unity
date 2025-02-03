using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using System.Net.WebSockets;

[Serializable]
public class SendTextFormat
{
    public string message;
}

public class WebSocketClient : MonoBehaviour
{
    [SerializeField] private bool DEBUG = true;  // デバッグモードのフラグ
    private ClientWebSocket webSocket;
    private bool isConnecting = false;
    private bool isConnected = false;
    private bool isQuitting = false;
    private bool Queue_is_empty = false;

    private void AddDebugMessages()
    {
        // コメント用のデバッグメッセージ
        GlobalVariables.CommentQueue.Add(new ReceiveMessageFormat
        {
            name = "comment",
            content = "こんにちは！配信楽しみです！",
            action = "Nothing",
            emotion = "aaa",
            scene = ""
        });
        GlobalVariables.CommentQueue.Add(new ReceiveMessageFormat
        {
            name = "comment",
            content = "かわいい！",
            action = "Nothing",
            emotion = "ooo",
            scene = ""
        });

        // 読み上げ用のデバッグメッセージ
        GlobalVariables.MessageQueue.Add(new ReceiveMessageFormat
        {
            name = "message",
            content = "これは読み上げテスト用のメッセージです",
            action = "Nothing",
            emotion = "",
            scene = ""
        });

        // Agent1用のデバッグメッセージ
        GlobalVariables.Agent1Queue.Add(new ReceiveMessageFormat
        {
            name = "agent1",
            content = "皆さん、今日も元気ですか？ 私は元気です！",
            action = "Nothing",
            emotion = "happy",
            scene = ""
        });
        GlobalVariables.Agent2Queue.Add(new ReceiveMessageFormat
        {
            name = "agent2",
            content = "あたしもめっちゃ元気！",
            action = "Nothing",
            emotion = "excited",
            scene = ""
        });
    }

    async void Start()
    {
        if (DEBUG)
        {
            AddDebugMessages();
            Debug.Log("Debug mode: Added test messages to queues");
            return;
        }

        await ConnectToServer();
        if (isConnected)
        {
            await SendText("Hello from Unity!");
        }
    }

    async void Update()
    {
        int count = GlobalVariables.MessageQueue.Count + GlobalVariables.Agent1Queue.Count + GlobalVariables.Agent2Queue.Count;
        // Queueが空になったらFinishを送信
        if (count == 0 && !Queue_is_empty)
        {
            Queue_is_empty = true;
            await SendText("Finish");
        }else if (count > 0)
        {
            Queue_is_empty = false;
        }
    }

    private async Task ConnectToServer()
    {
        if (isConnecting) return;
        isConnecting = true;
        webSocket = new ClientWebSocket();
        Uri serverUri = new Uri("ws://localhost:5000");
        try
        {
            await webSocket.ConnectAsync(serverUri, CancellationToken.None);
            Debug.Log("Connected to server");
            isConnected = true;
            StartReceiving();
        }
        catch (Exception e)
        {
            Debug.LogError($"WebSocket connection error: {e.Message}");
            Cleanup();
            await Task.Delay(5000); // Wait before retrying
            await ConnectToServer(); // Retry connection
        }
        finally
        {
            isConnecting = false;
        }
    }

    private void Cleanup()
    {
        if (webSocket != null)
        {
            try
            {
                if (webSocket.State == WebSocketState.Open)
                {
                    webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None).Wait();
                }
                webSocket.Dispose();
            }
            catch (Exception e)
            {
                Debug.LogError($"Error during cleanup: {e.Message}");
            }
            webSocket = null;
        }
        isConnected = false;
    }

    private async Task SendText(string message)
    {
        if (webSocket == null || webSocket.State != WebSocketState.Open)
        {
            Debug.LogWarning("Cannot send message - WebSocket is not connected");
            return;
        }
        {
            var messageObj = new SendTextFormat { message = message };
            string jsonMessage = JsonUtility.ToJson(messageObj);
            byte[] buffer = Encoding.UTF8.GetBytes(jsonMessage);
            await webSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
            Debug.Log($"Message sent: {jsonMessage}");
        }
    }

    private async void StartReceiving()
    {
        if (webSocket == null || webSocket.State != WebSocketState.Open)
        {
            Debug.LogWarning("Cannot start receiving - WebSocket is not connected");
            return;
        }

        byte[] buffer = new byte[4096]; // Increased buffer size
        while (webSocket != null && webSocket.State == WebSocketState.Open)
        {
            try
            {
                WebSocketReceiveResult result = await webSocket.ReceiveAsync(
                    new ArraySegment<byte>(buffer),
                    CancellationToken.None);

                if (result.MessageType == WebSocketMessageType.Close)
                {
                    Debug.Log("Server requested connection close");
                    await webSocket.CloseAsync(
                        WebSocketCloseStatus.NormalClosure,
                        "Closing",
                        CancellationToken.None);
                    break;
                }

                string jsonMessage = Encoding.UTF8.GetString(buffer, 0, result.Count);
                try
                {
                    var messageObj = JsonUtility.FromJson<ReceiveMessageFormat>(jsonMessage);
                    if (messageObj != null)
                    {
                        if (!string.IsNullOrEmpty(messageObj.content)){
                            Debug.Log($"Message received: {messageObj.content}");
                            if (messageObj.name == "comment") // Youtubeのコメント
                            {
                                GlobalVariables.CommentQueue.Add(messageObj);
                            }
                            else if (messageObj.name == "message") // Booyomiで読み上げるメッセージ
                            {
                                GlobalVariables.MessageQueue.Add(messageObj);
                            }
                            else if (messageObj.name == "agent1") // agent1の発言
                            {
                                GlobalVariables.Agent1Queue.Add(messageObj);
                            }
                            else if (messageObj.name == "agent2") // agent2の発言
                            {
                                GlobalVariables.Agent2Queue.Add(messageObj);
                            }
                        }
                        if (!string.IsNullOrEmpty(messageObj.scene))
                        {
                            if (messageObj.scene == "debate")
                            {
                                GlobalVariables.sceneIdx = 1;
                            }
                            else if (messageObj.scene == "conversation")
                            {
                                GlobalVariables.sceneIdx = 0;
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError($"Error parsing JSON message: {e.Message}");
                }
            }
            catch (WebSocketException e)
            {
                Debug.LogError($"WebSocket error: {e.Message}");
                await HandleDisconnection();
                break;
            }
            catch (Exception e)
            {
                Debug.LogError($"Error receiving message: {e.Message}");
                await HandleDisconnection();
                break;
            }
        }
    }

    private async Task HandleDisconnection()
    {
        Cleanup();
        if (!isQuitting)
        {
            Debug.Log("Attempting to reconnect...");
            await Task.Delay(3000); // Wait before reconnecting
            await ConnectToServer();
        }
        else
        {
            Debug.Log("Application is quitting - skipping reconnection");
        }
    }

    private void OnApplicationQuit()
    {
        Debug.Log("Application quitting - cleaning up WebSocket connection");
        isQuitting = true;
        Cleanup();
    }
}
