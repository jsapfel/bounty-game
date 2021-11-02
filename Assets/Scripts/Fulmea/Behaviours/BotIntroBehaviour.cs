using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotIntroBehaviour : StateMachineBehaviour
{
    public GameObject boltWarning;

    FulmeaController fulmea;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        fulmea = animator.GetComponent<FulmeaController>();
        fulmea.isInvincible = true;
        if(animator.GetBool("Stage2")) fulmea.StartIntroAttackCoroutine(boltWarning);
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    // override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    // {
    // }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(animator.GetBool("Stage2")) fulmea.StopIntroAttackCoroutine();
        fulmea.isInvincible = false;
        fulmea.attacking = false;
    }
}
