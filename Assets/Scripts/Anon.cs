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
                faceMR.SetBlendShapeWeight((int)AnonMorph.warai, 100f);
                faceMR.SetBlendShapeWeight((int)AnonMorph.nikori, 100f);
                break;
            case "angry":
                animator.SetInteger("EmotionIdx", (int)Emotion.angry);
                faceMR.SetBlendShapeWeight((int)AnonMorph.okori, 100f);
                faceMR.SetBlendShapeWeight((int)AnonMorph.niramu, 100f);
                faceMR.SetBlendShapeWeight((int)AnonMorph.high_light_off, 100f);
                break;
            case "sad":
                animator.SetInteger("EmotionIdx", (int)Emotion.sad);
                faceMR.SetBlendShapeWeight((int)AnonMorph.komaru, 100f);
                faceMR.SetBlendShapeWeight((int)AnonMorph.mayu_sita, 60f);
                break;
            case "surprised":
                animator.SetInteger("EmotionIdx", (int)Emotion.surprised);
                faceMR.SetBlendShapeWeight((int)AnonMorph.bikkuri, 50f);
                faceMR.SetBlendShapeWeight((int)AnonMorph.hitomi_small, 40f);
                break;
            case "shy":
                animator.SetInteger("EmotionIdx", (int)Emotion.shy);
                faceMR.SetBlendShapeWeight((int)AnonMorph.hohozome, 100f);
                faceMR.SetBlendShapeWeight((int)AnonMorph.komaru, 70f);
                break;
            case "excited":
                animator.SetInteger("EmotionIdx", (int)Emotion.excited);
                faceMR.SetBlendShapeWeight((int)AnonMorph.star, 100f);
                break;
            case "smug":
                animator.SetInteger("EmotionIdx", (int)Emotion.smug);
                faceMR.SetBlendShapeWeight((int)AnonMorph.okori, 50f);
                faceMR.SetBlendShapeWeight((int)AnonMorph.zitome, 80f);
                break;
            case "calm":
                animator.SetInteger("EmotionIdx", (int)Emotion.calm);
                faceMR.SetBlendShapeWeight((int)AnonMorph.nagomi, 15f);
                break;
        }
    }

    protected override void ResetEmotion()
    {
        faceMR.SetBlendShapeWeight((int)AnonMorph.warai, 0f);
        faceMR.SetBlendShapeWeight((int)AnonMorph.nikori, 0f);
        faceMR.SetBlendShapeWeight((int)AnonMorph.okori, 0f);
        faceMR.SetBlendShapeWeight((int)AnonMorph.niramu, 0f);
        faceMR.SetBlendShapeWeight((int)AnonMorph.high_light_off, 0f);
        faceMR.SetBlendShapeWeight((int)AnonMorph.komaru, 0f);
        faceMR.SetBlendShapeWeight((int)AnonMorph.mayu_sita, 0f);
        faceMR.SetBlendShapeWeight((int)AnonMorph.bikkuri, 0f);
        faceMR.SetBlendShapeWeight((int)AnonMorph.hitomi_small, 0f);
        faceMR.SetBlendShapeWeight((int)AnonMorph.hohozome, 0f);
        faceMR.SetBlendShapeWeight((int)AnonMorph.star, 0f);
        faceMR.SetBlendShapeWeight((int)AnonMorph.zitome, 0f);
        faceMR.SetBlendShapeWeight((int)AnonMorph.nagomi, 0f);
    }

    protected override Color GetTelopColor()
    {
        return new Color(0.5f, 0f, 0.5f); // Purple color (RGB: 128, 0, 128)
    }
}
