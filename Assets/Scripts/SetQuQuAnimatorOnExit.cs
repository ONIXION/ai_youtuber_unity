using UnityEngine;

public class SetQuQuAnimatorOnExit : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("QuQuのモーションを待機状態に戻します");
        animator.SetInteger("QuQuEmotionIdx", (int)Emotion.waiting);
    }
}