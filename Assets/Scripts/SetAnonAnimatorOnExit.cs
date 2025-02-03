using UnityEngine;

public class SetAnonAnimatorOnExit : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Anonのモーションを待機状態に戻します");
        animator.SetInteger("AnonEmotionIdx", (int)Emotion.waiting);
    }
}