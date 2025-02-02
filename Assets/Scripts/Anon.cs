using UnityEngine;
using System.Collections.Generic;

public class Anon : AivisSpeechCharacter
{
    protected override List<ReceiveMessageFormat> AgentQueue => GlobalVariables.Agent1Queue;

    protected override void ApplyEmotion(string emotion)
    {
        switch (emotion)
        {
            case "normal":
                animator.SetInteger("EmotionIdx", (int)Emotion.normal);
                break;
            case "happy":
                animator.SetInteger("EmotionIdx", (int)Emotion.happy);
                faceMR.SetBlendShapeWeight((int)Morph.warai, 100f);
                faceMR.SetBlendShapeWeight((int)Morph.nikori, 100f);
                break;
            case "angry":
                animator.SetInteger("EmotionIdx", (int)Emotion.angry);
                faceMR.SetBlendShapeWeight((int)Morph.okori, 100f);
                faceMR.SetBlendShapeWeight((int)Morph.niramu, 100f);
                faceMR.SetBlendShapeWeight((int)Morph.high_light_off, 100f);
                break;
            case "sad":
                animator.SetInteger("EmotionIdx", (int)Emotion.sad);
                faceMR.SetBlendShapeWeight((int)Morph.komaru, 100f);
                faceMR.SetBlendShapeWeight((int)Morph.mayu_sita, 60f);
                break;
            case "surprised":
                animator.SetInteger("EmotionIdx", (int)Emotion.surprised);
                faceMR.SetBlendShapeWeight((int)Morph.bikkuri, 50f);
                faceMR.SetBlendShapeWeight((int)Morph.hitomi_small, 40f);
                break;
            case "shy":
                animator.SetInteger("EmotionIdx", (int)Emotion.shy);
                faceMR.SetBlendShapeWeight((int)Morph.hohozome, 100f);
                faceMR.SetBlendShapeWeight((int)Morph.komaru, 70f);
                break;
            case "excited":
                animator.SetInteger("EmotionIdx", (int)Emotion.excited);
                faceMR.SetBlendShapeWeight((int)Morph.star, 100f);
                break;
            case "smug":
                animator.SetInteger("EmotionIdx", (int)Emotion.smug);
                faceMR.SetBlendShapeWeight((int)Morph.okori, 50f);
                faceMR.SetBlendShapeWeight((int)Morph.zitome, 80f);
                break;
            case "calm":
                animator.SetInteger("EmotionIdx", (int)Emotion.calm);
                faceMR.SetBlendShapeWeight((int)Morph.nagomi, 15f);
                break;
        }
    }

    protected override void ResetEmotion()
    {
        faceMR.SetBlendShapeWeight((int)Morph.warai, 0f);
        faceMR.SetBlendShapeWeight((int)Morph.nikori, 0f);
        faceMR.SetBlendShapeWeight((int)Morph.okori, 0f);
        faceMR.SetBlendShapeWeight((int)Morph.niramu, 0f);
        faceMR.SetBlendShapeWeight((int)Morph.high_light_off, 0f);
        faceMR.SetBlendShapeWeight((int)Morph.komaru, 0f);
        faceMR.SetBlendShapeWeight((int)Morph.mayu_sita, 0f);
        faceMR.SetBlendShapeWeight((int)Morph.bikkuri, 0f);
        faceMR.SetBlendShapeWeight((int)Morph.hitomi_small, 0f);
        faceMR.SetBlendShapeWeight((int)Morph.hohozome, 0f);
        faceMR.SetBlendShapeWeight((int)Morph.star, 0f);
        faceMR.SetBlendShapeWeight((int)Morph.zitome, 0f);
        faceMR.SetBlendShapeWeight((int)Morph.nagomi, 0f);
    }
}
