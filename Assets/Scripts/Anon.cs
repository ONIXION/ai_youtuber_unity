using UnityEngine;
using System.Collections.Generic;

public class Anon : AivisSpeechCharacter
{
    protected override List<ReceiveMessageFormat> AgentQueue => GlobalVariables.Agent2Queue;

    protected override void HandleAction(string action)
    {
        switch (action)
        {
            case "Think":
                faceMR.SetBlendShapeWeight((int)AnonMorph.manpu_hatena, 100f);
                animator.SetBool("AnonIsThinking", true);
                animator.SetBool("AnonIsSearching", false);
                break;
            case "WebSearch":
                faceMR.SetBlendShapeWeight((int)AnonMorph.manpu_hatena, 0f);
                animator.SetBool("AnonIsSearching", true);
                animator.SetBool("AnonIsThinking", false);
                break;
            case "Nothing":
                faceMR.SetBlendShapeWeight((int)AnonMorph.manpu_hatena, 0f);
                animator.SetBool("AnonIsThinking", false);
                animator.SetBool("AnonIsSearching", false);
                break;
        }
    }

    protected override void ApplyEmotion(string emotion)
    {
        switch (emotion)
        {
            case "normal":
                Debug.Log("AnonEmotionIdx: normal");
                animator.SetInteger("AnonEmotionIdx", (int)Emotion.normal);
                break;
            case "happy":
                Debug.Log("AnonEmotionIdx: happy");
                animator.SetInteger("AnonEmotionIdx", (int)Emotion.happy);
                faceMR.SetBlendShapeWeight((int)AnonMorph.eye_smile, 100f);
                faceMR.SetBlendShapeWeight((int)AnonMorph.mayu_joy, 100f);
                faceMR.SetBlendShapeWeight((int)AnonMorph.manpu_waiwai, 100f);
                break;
            case "angry":
                Debug.Log("AnonEmotionIdx: angry");
                animator.SetInteger("AnonEmotionIdx", (int)Emotion.angry);
                faceMR.SetBlendShapeWeight((int)AnonMorph.eye_angly, 100f);
                faceMR.SetBlendShapeWeight((int)AnonMorph.highlight_hide, 100f);
                faceMR.SetBlendShapeWeight((int)AnonMorph.mayu_anger, 100f);
                faceMR.SetBlendShapeWeight((int)AnonMorph.namida, 100f);
                faceMR.SetBlendShapeWeight((int)AnonMorph.manpu_anger, 100f);
                break;
            case "sad":
                Debug.Log("AnonEmotionIdx: sad");
                animator.SetInteger("AnonEmotionIdx", (int)Emotion.sad);
                faceMR.SetBlendShapeWeight((int)AnonMorph.eye_sad, 100f);
                faceMR.SetBlendShapeWeight((int)AnonMorph.mayu_trouble, 100f);
                faceMR.SetBlendShapeWeight((int)AnonMorph.manpu_gaan, 100f);
                break;
            case "surprised":
                Debug.Log("AnonEmotionIdx: surprised");
                animator.SetInteger("AnonEmotionIdx", (int)Emotion.surprised);
                faceMR.SetBlendShapeWeight((int)AnonMorph.eye_open, 100f);
                faceMR.SetBlendShapeWeight((int)AnonMorph.eye_small, 100f);
                faceMR.SetBlendShapeWeight((int)AnonMorph.mayu_joy, 100f);
                faceMR.SetBlendShapeWeight((int)AnonMorph.manpu_bikkuri, 100f);
                break;
            case "shy":
                Debug.Log("AnonEmotionIdx: shy");
                animator.SetInteger("AnonEmotionIdx", (int)Emotion.shy);
                faceMR.SetBlendShapeWeight((int)AnonMorph.eye_niya, 20f);
                faceMR.SetBlendShapeWeight((int)AnonMorph.eye_tare, 100f);
                faceMR.SetBlendShapeWeight((int)AnonMorph.mayu_trouble, 50f);
                faceMR.SetBlendShapeWeight((int)AnonMorph.cheek_tere, 100f);
                break;
            case "excited":
                Debug.Log("AnonEmotionIdx: excited");
                animator.SetInteger("AnonEmotionIdx", (int)Emotion.excited);
                faceMR.SetBlendShapeWeight((int)AnonMorph.eye_open, 100f);
                faceMR.SetBlendShapeWeight((int)AnonMorph.eye_hoshi, 100f);
                faceMR.SetBlendShapeWeight((int)AnonMorph.mayu_joy, 100f);
                faceMR.SetBlendShapeWeight((int)AnonMorph.manpu_waiwai, 100f);
                break;
            case "smug":
                Debug.Log("AnonEmotionIdx: smug");
                animator.SetInteger("AnonEmotionIdx", (int)Emotion.smug); // どや顔
                faceMR.SetBlendShapeWeight((int)AnonMorph.eye_jito, 100f);
                faceMR.SetBlendShapeWeight((int)AnonMorph.mayu_anger, 50f);
                break;
            case "calm":
                Debug.Log("AnonEmotionIdx: calm");
                animator.SetInteger("AnonEmotionIdx", (int)Emotion.calm);
                faceMR.SetBlendShapeWeight((int)AnonMorph.eye_nagomi, 100f);
                faceMR.SetBlendShapeWeight((int)AnonMorph.mayu_tare, 100f);
                break;
        }
    }

    protected override void ResetEmotion()
    {
        // 変更したモーフをリセット
        faceMR.SetBlendShapeWeight((int)AnonMorph.eye_smile, 0f);
        faceMR.SetBlendShapeWeight((int)AnonMorph.eye_angly, 0f);
        faceMR.SetBlendShapeWeight((int)AnonMorph.eye_sad, 0f);
        faceMR.SetBlendShapeWeight((int)AnonMorph.eye_open, 0f);
        faceMR.SetBlendShapeWeight((int)AnonMorph.eye_nagomi, 0f);
        faceMR.SetBlendShapeWeight((int)AnonMorph.eye_niya, 0f);
        faceMR.SetBlendShapeWeight((int)AnonMorph.eye_hoshi, 0f);
        faceMR.SetBlendShapeWeight((int)AnonMorph.eye_tare, 0f);
        faceMR.SetBlendShapeWeight((int)AnonMorph.mayu_joy, 0f);
        faceMR.SetBlendShapeWeight((int)AnonMorph.mayu_anger, 0f);
        faceMR.SetBlendShapeWeight((int)AnonMorph.mayu_trouble, 0f);
        faceMR.SetBlendShapeWeight((int)AnonMorph.cheek_tere, 0f);
        faceMR.SetBlendShapeWeight((int)AnonMorph.manpu_bikkuri, 0f);
        faceMR.SetBlendShapeWeight((int)AnonMorph.manpu_gaan, 0f);
        faceMR.SetBlendShapeWeight((int)AnonMorph.manpu_waiwai, 0f);
        faceMR.SetBlendShapeWeight((int)AnonMorph.manpu_anger, 0f);
        faceMR.SetBlendShapeWeight((int)AnonMorph.highlight_hide, 0f);
        faceMR.SetBlendShapeWeight((int)AnonMorph.namida, 0f);
        faceMR.SetBlendShapeWeight((int)AnonMorph.eye_small, 0f);
        faceMR.SetBlendShapeWeight((int)AnonMorph.eye_jito, 0f);
        faceMR.SetBlendShapeWeight((int)AnonMorph.mayu_tare, 0f);
        animator.SetTrigger("AnonFinishTalk");
        Debug.Log("Anonの発話が終了しました");
    }

    protected override Color GetTelopColor()
    {
        return new Color(0.5f, 0f, 0.5f); // Purple color (RGB: 128, 0, 128)
    }
}