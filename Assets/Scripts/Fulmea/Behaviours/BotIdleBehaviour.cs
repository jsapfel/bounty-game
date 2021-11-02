using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotIdleBehaviour : StateMachineBehaviour
{
    public float minIdleTime;
    public float maxIdleTime;
    
    float idleTimer;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {        
        if(animator.GetBool("Continue")) idleTimer = animator.GetFloat("MoveTime");
        else
        {
            idleTimer = Random.Range(minIdleTime, maxIdleTime);
            animator.SetBool("Continue", true);
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        idleTimer -= Time.deltaTime;
        if(idleTimer < 0)
        {
            animator.SetBool("Continue", false);
            animator.SetBool("Moving", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(animator.GetBool("Continue")) animator.SetFloat("MoveTime", idleTimer);
    }
}
