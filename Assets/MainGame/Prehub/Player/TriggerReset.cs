using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerReset : StateMachineBehaviour
{
    [SerializeField] string triggerName1;
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       animator.ResetTrigger(triggerName1);
       animator.SetInteger("AttackType",0);
    }
}
