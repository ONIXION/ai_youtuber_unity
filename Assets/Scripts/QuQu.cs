using UnityEngine;
using System.Collections.Generic;

public class QuQu : AivisSpeechCharacter
{
    protected override List<ReceiveMessageFormat> AgentQueue => GlobalVariables.Agent1Queue;

    protected override void HandleAction(string action)
    {
        switch (action)
        {
            case "Think":
                animator.SetBool("QuQuIsThinking", true);
                animator.SetBool("QuQuIsSearching", false);
                break;
            case "WebSearch":
                animator.SetBool("QuQuIsSearching", true);
                animator.SetBool("QuQuIsThinking", false);
                break;
            case "Nothing":
                animator.SetBool("QuQuIsThinking", false);
                animator.SetBool("QuQuIsSearching", false);
                break;
        }
    }

    protected override void ApplyEmotion(string emotion)
    {
        switch (emotion)
        {
            case "normal":
                Debug.Log("QuQuEmotionIdx: normal");
                animator.SetInteger("QuQuEmotionIdx", (int)Emotion.normal);
                break;
            case "happy":
                Debug.Log("QuQuEmotionIdx: happy");
                animator.SetInteger("QuQuEmotionIdx", (int)Emotion.happy);
                faceMR.SetBlendShapeWeight((int)QuQuMorph.warai, 100f);
                faceMR.SetBlendShapeWeight((int)QuQuMorph.nikori, 100f);
                break;
            case "angry":
                Debug.Log("QuQuEmotionIdx: angry");
                animator.SetInteger("QuQuEmotionIdx", (int)Emotion.angry);
                faceMR.SetBlendShapeWeight((int)QuQuMorph.okori, 100f);
                faceMR.SetBlendShapeWeight((int)QuQuMorph.niramu, 100f);
                faceMR.SetBlendShapeWeight((int)QuQuMorph.high_light_off, 100f);
                break;
            case "sad":
                Debug.Log("QuQuEmotionIdx: sad");
                animator.SetInteger("QuQuEmotionIdx", (int)Emotion.sad);
                faceMR.SetBlendShapeWeight((int)QuQuMorph.komaru, 100f);
                faceMR.SetBlendShapeWeight((int)QuQuMorph.mayu_sita, 60f);
                break;
            case "surprised":
                Debug.Log("QuQuEmotionIdx: surprised");
                animator.SetInteger("QuQuEmotionIdx", (int)Emotion.surprised);
                faceMR.SetBlendShapeWeight((int)QuQuMorph.bikkuri, 50f);
                faceMR.SetBlendShapeWeight((int)QuQuMorph.hitomi_small, 40f);
                break;
            case "shy":
                Debug.Log("QuQuEmotionIdx: shy");
                animator.SetInteger("QuQuEmotionIdx", (int)Emotion.shy);
                faceMR.SetBlendShapeWeight((int)QuQuMorph.hohozome, 100f);
                faceMR.SetBlendShapeWeight((int)QuQuMorph.komaru, 70f);
                break;
            case "excited":
                Debug.Log("QuQuEmotionIdx: excited");
                animator.SetInteger("QuQuEmotionIdx", (int)Emotion.excited);
                faceMR.SetBlendShapeWeight((int)QuQuMorph.star, 100f);
                break;
            case "smug":
                Debug.Log("QuQuEmotionIdx: smug");
                animator.SetInteger("QuQuEmotionIdx", (int)Emotion.smug);
                faceMR.SetBlendShapeWeight((int)QuQuMorph.okori, 50f);
                faceMR.SetBlendShapeWeight((int)QuQuMorph.zitome, 80f);
                break;
            case "calm":
                Debug.Log("QuQuEmotionIdx: calm");
                animator.SetInteger("QuQuEmotionIdx", (int)Emotion.calm);
                faceMR.SetBlendShapeWeight((int)QuQuMorph.nagomi, 15f);
                break;
        }
    }

    protected override void ResetEmotion()
    {
        faceMR.SetBlendShapeWeight((int)QuQuMorph.warai, 0f);
        faceMR.SetBlendShapeWeight((int)QuQuMorph.nikori, 0f);
        faceMR.SetBlendShapeWeight((int)QuQuMorph.okori, 0f);
        faceMR.SetBlendShapeWeight((int)QuQuMorph.niramu, 0f);
        faceMR.SetBlendShapeWeight((int)QuQuMorph.high_light_off, 0f);
        faceMR.SetBlendShapeWeight((int)QuQuMorph.komaru, 0f);
        faceMR.SetBlendShapeWeight((int)QuQuMorph.mayu_sita, 0f);
        faceMR.SetBlendShapeWeight((int)QuQuMorph.bikkuri, 0f);
        faceMR.SetBlendShapeWeight((int)QuQuMorph.hitomi_small, 0f);
        faceMR.SetBlendShapeWeight((int)QuQuMorph.hohozome, 0f);
        faceMR.SetBlendShapeWeight((int)QuQuMorph.star, 0f);
        faceMR.SetBlendShapeWeight((int)QuQuMorph.zitome, 0f);
        faceMR.SetBlendShapeWeight((int)QuQuMorph.nagomi, 0f);
        animator.SetTrigger("QuQuFinishTalk");
        Debug.Log("QuQuの発話が終了しました");
    }

    protected override Color GetTelopColor()
    {
        // 暗めのオレンジを指定
        return Color.HSVToRGB(0.08f, 1.0f, 0.5f);
    }
}
