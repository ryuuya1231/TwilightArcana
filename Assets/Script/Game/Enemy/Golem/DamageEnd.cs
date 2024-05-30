using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class DamageEnd : StateMachineBehaviour
{
    private NavMeshAgent _navMeshAgent;
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _navMeshAgent = animator.gameObject.GetComponent<NavMeshAgent>();
        _navMeshAgent.updatePosition = true;
        _navMeshAgent.updateRotation = true;
        _navMeshAgent.speed = 3.5f;
    }
}
