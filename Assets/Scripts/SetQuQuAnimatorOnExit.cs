using UnityEngine;

public class SetQuQuAnimatorOnExit : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetFloat("QuQuEmotionIdx", 9);
    }
}