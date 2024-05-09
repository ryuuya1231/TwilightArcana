using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatReset : StateMachineBehaviour
{
    [SerializeField] string FloatName;
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger(FloatName);
    }
}
