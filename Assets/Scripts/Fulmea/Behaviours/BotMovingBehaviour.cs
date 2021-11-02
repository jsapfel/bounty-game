using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotMovingBehaviour : StateMachineBehaviour
{
    public float idealDistToPlayer;
    public float speed;
    public float minMoveTime;
    public float maxMoveTime;
    public float maxRandomness;

    float moveTimer;
    Rigidbody2D rb;
    Transform player;
    Vector2 dirPrevPosChange;
    float changeDirTimer = -1;
    Vector2 targetLoc;
    Vector2 dirToPlayer;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb = animator.GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").transform;

        if(animator.GetBool("Continue")) moveTimer = animator.GetFloat("MoveTime");
        else
        {
            moveTimer = Random.Range(minMoveTime, maxMoveTime);
            animator.SetBool("Continue", true);
        }
        //dirPrevPosChange = (rb.position - (Vector2)player.transform.position).normalized;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Vector2 targetLoc = player.position - (Vector2)(Quaternion.AngleAxis(Random.Range(-maxRandomness, maxRandomness), Vector3.forward) * dirToPlayer * idealDistToPlayer);
        // float angle = Vector2.SignedAngle(dirPrevPosChange, targetLoc - rb.position);
        // //IF ANGLE>90/<-90, NEXT MOVE IS SUPPOSED TO BE BACKWARDS FROM PREVIOUS
        // if(angle > 90) targetLoc = rb.position + (Vector2)(Quaternion.AngleAxis(90, Vector3.forward) * dirPrevPosChange) * speed;
        // if(angle < -90) targetLoc = rb.position + (Vector2)(Quaternion.AngleAxis(-90, Vector3.forward) * dirPrevPosChange) * speed;
        // //targetLoc += new Vector2(Random.Range(-maxRandomness, maxRandomness), Random.Range(-maxRandomness, maxRandomness));

        changeDirTimer -= Time.deltaTime;

        if(changeDirTimer < 0)
        {
            dirToPlayer = ((Vector2)player.position - rb.position).normalized;
            changeDirTimer = (minMoveTime+maxMoveTime)/2;
            int sign = (Random.value < 0.5f) ? -1 : 1;
            Vector2 locDir = Quaternion.AngleAxis(Random.Range(sign*(90-maxRandomness), sign*(90+maxRandomness)), Vector3.forward) * dirToPlayer;
            targetLoc = (Vector2)player.position - idealDistToPlayer*locDir + Random.insideUnitCircle*2;
        }
        else if(Vector2.Distance(rb.position, targetLoc) < 0.1f)
            targetLoc += (Vector2)(Quaternion.AngleAxis(Random.Range(-maxRandomness, maxRandomness), Vector3.forward) * dirPrevPosChange);

        Vector2 rbPrevPos = rb.position;
        rb.MovePosition(Vector2.MoveTowards(rb.position, targetLoc, speed*Time.fixedDeltaTime));
        dirPrevPosChange = (rb.position - rbPrevPos).normalized;

        moveTimer -= Time.deltaTime;
        if(moveTimer < 0)
        {
            animator.SetBool("Continue", false);
            animator.SetBool("Moving", false);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(animator.GetBool("Continue")) animator.SetFloat("MoveTime", moveTimer);
    }
}
