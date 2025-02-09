﻿// https://github.com/TORISOUP/UnityBoyomichanClient
// USE: https://chi.usamimi.info/Program/Application/BouyomiChan/
using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityBoyomichanClient;

public class BooyomiMessage : MonoBehaviour
{
    private CancellationTokenSource _cancellationTokenSource;
    private IBoyomichanClient _boyomichanClient;
    private const string DEFAULT_HOST = "127.0.0.1";
    private const int DEFAULT_PORT = 50001;
    [SerializeField]
    private int DEFAULT_SPEED = 100; // 50~200
    [SerializeField]
    private int DEFAULT_PITCH = 120; // 60~190
    [SerializeField]
    private int DEFAULT_VOLUME = 100; // 0~100
    [SerializeField]
    private VoiceType DEFAULT_VOICE_TYPE = VoiceType.Male1; // 棒読みちゃん画面上の設定を使用

    [SerializeField]
    private Telop telop;

    private void Start()
    {
        GlobalVariables.BooyomiState = 0;
        _cancellationTokenSource = new CancellationTokenSource();

        if (telop == null)
        {
            telop = FindObjectOfType<Telop>();
            if (telop == null)
            {
                Debug.LogError("Telop component not found in the scene!");
            }
        }
        // TCP接続を確立
        _boyomichanClient = new TcpBoyomichanClient(DEFAULT_HOST, DEFAULT_PORT);
        // メッセージキューの監視を開始
        CheckMessageQueue(_cancellationTokenSource.Token).Forget();
    }

    private async UniTask CheckMessageQueue(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            if (_boyomichanClient != null)
            {
                // メッセージキューにメッセージがある場合 かつ Aivisが停止中の場合 かつ Booyomiが停止中の場合
                if (GlobalVariables.MessageQueue.Count > 0 && GlobalVariables.AivisState == 0 && GlobalVariables.BooyomiState == 0)
                {
                    var message = GlobalVariables.MessageQueue[0];
                    GlobalVariables.MessageQueue.RemoveAt(0);
                    if (!string.IsNullOrEmpty(message.content))
                    {
                        GlobalVariables.BooyomiState = 1; // 音声合成中
                        // Display telop before speech
                        if (telop != null)
                        {
                            if (message.name == "message"){
                                telop.Display(message.content, Color.black, 0.13f).Forget();
                            }else if(message.name == "host"){
                                telop.Display(message.content, Color.red, 0.13f).Forget();
                            }
                        }
                        try
                        {
                            await _boyomichanClient.TalkAsync(
                                message.content,
                                DEFAULT_SPEED,
                                DEFAULT_PITCH,
                                DEFAULT_VOLUME,
                                DEFAULT_VOICE_TYPE,
                                token
                            );
                            bool isPlaying = false;
                            while (!token.IsCancellationRequested)
                            {
                                isPlaying = await _boyomichanClient.CheckNowPlayingAsync(token);
                                if (isPlaying)
                                {
                                    GlobalVariables.BooyomiState = 2; // 音声出力中に更新
                                    break;
                                }
                                await UniTask.Delay(TimeSpan.FromMilliseconds(50), cancellationToken: token);
                            }
                            while (!token.IsCancellationRequested)
                            {
                                isPlaying = await _boyomichanClient.CheckNowPlayingAsync(token);
                                if (!isPlaying)
                                {
                                    GlobalVariables.BooyomiState = 0; // 音声出力終了に更新
                                    break;
                                }
                                await UniTask.Delay(TimeSpan.FromMilliseconds(50), cancellationToken: token);
                            }
                        }
                        catch (Exception e)
                        {
                            Debug.LogError($"音声合成/再生中にエラーが発生しました: {e.Message}");
                            GlobalVariables.BooyomiState = 0; // エラー時は停止状態に
                        }
                    }
                }
            }
            await UniTask.Delay(TimeSpan.FromMilliseconds(100), cancellationToken: token);
        }
    }

    private void OnDestroy()
    {
        _boyomichanClient?.Dispose();
        _boyomichanClient = null;
        _cancellationTokenSource.Cancel();
        _cancellationTokenSource.Dispose();
    }
}
