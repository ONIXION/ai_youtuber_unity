using UnityEngine;
using Cysharp.Threading.Tasks;
using System;

public abstract class AivisSpeechCharacter : MonoBehaviour
{
    [SerializeField] protected AudioSource audioSource;
    [SerializeField] protected SkinnedMeshRenderer faceMR;
    [SerializeField] protected Animator animator;

    protected virtual void Start()
    {
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    protected virtual void Update()
    {
        if (GlobalVariables.AgentQueue.Count > 0 && GlobalVariables.AivisState == 0 && GlobalVariables.BooyomiState == 0)
        {
            var message = GlobalVariables.AgentQueue[0];
            GlobalVariables.AgentQueue.RemoveAt(0);
            
            HandleAction(message.action);
            Text2VoiceAsync(message.content, message.emotion).Forget();
        }
    }

    protected virtual void HandleAction(string action)
    {
        switch (action)
        {
            case "Think":
                animator.SetBool("isThinking", true);
                break;
            case "WebSearch":
                animator.SetBool("isSearching", true);
                break;
            case "Nothing":
                animator.SetBool("isThinking", false);
                animator.SetBool("isSearching", false);
                break;
        }
    }

    protected abstract void ApplyEmotion(string emotion);
    protected abstract void ResetEmotion();

    protected virtual async UniTask Text2VoiceAsync(string text, string emotion)
    {
        try
        {
            ApplyEmotion(emotion);

            var audioData = await AivisSpeechClient.Instance.Text2VoiceAsync(text, emotion);
            if (audioData == null) return;

            if (emotion == "surprised")
            {
                animator.SetInteger("EmotionIdx", (int)Emotion.waiting);
            }

            var audioClip = AivisSpeechClient.CreateAudioClipFromWAV(audioData);
            if (audioClip == null) return;

            audioSource.clip = audioClip;
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
            ResetEmotion();
            animator.SetTrigger("FinishTalk");
            GlobalVariables.AivisState = 0;
        }
    }
}
