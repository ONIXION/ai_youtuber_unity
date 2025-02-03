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
                faceMR.SetBlendShapeWeight((int)AnonMorph.manpu_hatena, 100f);
                animator.SetBool("QuQuIsThinking", true);
                animator.SetBool("QuQuIsSearching", false);
                break;
            case "WebSearch":
                faceMR.SetBlendShapeWeight((int)AnonMorph.manpu_hatena, 0f);
                animator.SetBool("QuQuIsSearching", true);
                animator.SetBool("QuQuIsThinking", false);
                break;
            case "Nothing":
                faceMR.SetBlendShapeWeight((int)AnonMorph.manpu_hatena, 0f);
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
                animator.SetInteger("QuQuEmotionIdx", (int)Emotion.normal);
                break;
            case "happy":
                animator.SetInteger("QuQuEmotionIdx", (int)Emotion.happy);
                faceMR.SetBlendShapeWeight((int)QuQuMorph.warai, 100f);
                faceMR.SetBlendShapeWeight((int)QuQuMorph.nikori, 100f);
                break;
            case "angry":
                animator.SetInteger("QuQuEmotionIdx", (int)Emotion.angry);
                faceMR.SetBlendShapeWeight((int)QuQuMorph.okori, 100f);
                faceMR.SetBlendShapeWeight((int)QuQuMorph.niramu, 100f);
                faceMR.SetBlendShapeWeight((int)QuQuMorph.high_light_off, 100f);
                break;
            case "sad":
                animator.SetInteger("QuQuEmotionIdx", (int)Emotion.sad);
                faceMR.SetBlendShapeWeight((int)QuQuMorph.komaru, 100f);
                faceMR.SetBlendShapeWeight((int)QuQuMorph.mayu_sita, 60f);
                break;
            case "surprised":
                animator.SetInteger("QuQuEmotionIdx", (int)Emotion.surprised);
                faceMR.SetBlendShapeWeight((int)QuQuMorph.bikkuri, 50f);
                faceMR.SetBlendShapeWeight((int)QuQuMorph.hitomi_small, 40f);
                break;
            case "shy":
                animator.SetInteger("QuQuEmotionIdx", (int)Emotion.shy);
                faceMR.SetBlendShapeWeight((int)QuQuMorph.hohozome, 100f);
                faceMR.SetBlendShapeWeight((int)QuQuMorph.komaru, 70f);
                break;
            case "excited":
                animator.SetInteger("QuQuEmotionIdx", (int)Emotion.excited);
                faceMR.SetBlendShapeWeight((int)QuQuMorph.star, 100f);
                break;
            case "smug":
                animator.SetInteger("QuQuEmotionIdx", (int)Emotion.smug);
                faceMR.SetBlendShapeWeight((int)QuQuMorph.okori, 50f);
                faceMR.SetBlendShapeWeight((int)QuQuMorph.zitome, 80f);
                break;
            case "calm":
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
    }

    protected override Color GetTelopColor()
    {
        return Color.green;
    }
}
