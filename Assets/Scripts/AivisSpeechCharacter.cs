using UnityEngine;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;

public abstract class AivisSpeechCharacter : MonoBehaviour
{
    [SerializeField] protected AudioSource audioSource;
    [SerializeField] protected SkinnedMeshRenderer faceMR;
    [SerializeField] protected Animator animator;
    [SerializeField] protected Telop telop;
    /// <summary>
    /// 話速 0.5~2.0
    /// </summary>
    [SerializeField] protected float speedScale = 1.0f;
    /// <summary>
    /// スタイルの強さ 0.0~2.0
    /// </summary>
    [SerializeField] protected float intonationScale = 1.0f;
    /// <summary>
    /// 緩急の強さ 0.0~2.0
    /// </summary>
    [SerializeField] protected float tempoDynamicsScale = 1.0f;
    /// <summary>
    /// 声の高さ -0.15~0.15
    /// </summary>
    [SerializeField] protected float pitchScale = 0.0f;
    /// <summary>
    /// 音量 0.0~2.0
    /// </summary>
    [SerializeField] protected float volumeScale = 1.0f;
    /// <summary>
    /// テロップの文字ごとの表示間隔
    /// </summary>
    [SerializeField] protected float characterInterval = 0.1f;
    /// <summary>
    /// テロップの全ての文字を表示した後に表示を維持する時間
    /// </summary>
    [SerializeField] protected float displayDuration = 2f;

    protected virtual void Start()
    {
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        if (telop == null)
        {
            telop = FindObjectOfType<Telop>();
            if (telop == null)
            {
                Debug.LogError("Telop component not found in the scene!");
            }
        }
    }

    protected abstract List<ReceiveMessageFormat> AgentQueue { get; }

    protected virtual void Update()
    {
        // AgentNQueueにメッセージがある場合 かつ AivisStateが停止中 かつ BooyomiStateが停止中
        if (AgentQueue.Count > 0 && GlobalVariables.AivisState == 0 && GlobalVariables.BooyomiState == 0)
        {
            GlobalVariables.AivisState = 1; // 音声合成中
            var message = AgentQueue[0];
            AgentQueue.RemoveAt(0);
            HandleAction(message.action);
            Text2VoiceAsync(
                message.content,
                message.emotion,
                message.action,
                speedScale,
                intonationScale,
                tempoDynamicsScale,
                pitchScale,
                volumeScale
            ).Forget();
        }
    }

    protected abstract void HandleAction(string action);
    protected abstract void ApplyEmotion(string emotion);
    protected abstract void ResetEmotion();

    protected virtual async UniTask Text2VoiceAsync(
        string text,
        string emotion,
        string action,
        float speedScale = 1.0f,
        float intonationScale = 1.0f,
        float tempoDynamicsScale = 1.0f,
        float pitchScale = 0.0f,
        float volumeScale = 1.0f)
    {
        try
        {
            // actionがThinkかWebSearchの場合は表情を変えない
            if (action != "Think" && action != "WebSearch"){
                ApplyEmotion(emotion);
            }

            var audioData = await AivisSpeechClient.Instance.Text2VoiceAsync(
                text,
                emotion,
                speedScale,
                intonationScale,
                tempoDynamicsScale,
                pitchScale,
                volumeScale);

            if (audioData == null) return;

            var audioClip = AivisSpeechClient.CreateAudioClipFromWAV(audioData);
            if (audioClip == null) return;

            // Display telop before playing audio
            if (telop != null)
            {
                // Get the appropriate color based on the character type
                Color textColor = GetTelopColor();
                telop.Display(text, textColor, characterInterval, displayDuration).Forget();
            }

            audioSource.clip = audioClip;
            Debug.Log($"Playing Aivis audio: {text}");
            GlobalVariables.AivisState = 2; // 音声出力中
            audioSource.Play();

            while (audioSource.isPlaying)
            {
                await UniTask.Yield();
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Error in Text2Voice: {e.Message}");
        }
        finally
        {
            Debug.Log("Aivis speech finished");
            ResetEmotion();
            GlobalVariables.AivisState = 0;
        }
    }

    protected virtual Color GetTelopColor()
    {
        // Default implementation returns black
        // Override in QuQu and Anon to return their specific colors
        return Color.black;
    }
}
