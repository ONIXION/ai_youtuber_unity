using UnityEngine;

public class SetAnonAnimatorOnExit : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetFloat("AnonEmotionIdx", 9);
    }
}