using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotWarningBehaviour : StateMachineBehaviour
{
    public GameObject boltWarningPrefab;
    public GameObject stormWarningPrefab;

    public GameObject lightningPulsePrefab;
    public GameObject lightningChannelPrefab;
    public GameObject lightningBoltPrefab;
    public GameObject stormPrefab;
    
    public float closeRange;
    public float midRange;

    Transform player;
    Vector2 playerPos;
    Rigidbody2D rb;
    FulmeaController fulmea;
    int attack = -1;
    float warningTimer;
    Vector2 boltStormOffset = 0.2f * Vector2.up;

    GameObject warning;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb = animator.GetComponent<Rigidbody2D>();
        fulmea = animator.GetComponent<FulmeaController>();
        player = GameObject.FindWithTag("Player").transform;

        playerPos = player.position;

        float distToPlayer = (rb.position - playerPos).magnitude;
        if(distToPlayer < closeRange) attack = 0;                                                   // 0:pulse, 1:channel, 2:bolt, 3:storm
        else if(distToPlayer < midRange) attack = (Random.value < 0.75f) ? 1 : Random.Range(2,4);
        else attack = Random.Range(2,4);

        if(attack == 1 || attack == 2) warningTimer = 0.5f;
        else warningTimer = 1f;

         //= new GameObject();
        if(attack == 2)
            warning = Instantiate(boltWarningPrefab, playerPos + boltStormOffset, Quaternion.identity, player);
        else if(attack == 3)
            warning = Instantiate(stormWarningPrefab, playerPos + boltStormOffset, Quaternion.identity, player);
        if(warning != null)
        {
            Destroy(warning, warningTimer);
        }

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        warningTimer -= Time.deltaTime;
        if(warningTimer < 0) animator.SetBool("Warning", false);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(attack == 0) Instantiate(lightningPulsePrefab, rb.position + 0.6f*Vector2.up, Quaternion.identity);
        else if(attack == 1)
        {
            GameObject lightningChannel =
                Instantiate(lightningChannelPrefab, rb.position + 0.6f*Vector2.up, Quaternion.identity, animator.GetComponent<Transform>());
            fulmea.attackCooldown += 2.3f;
        }

        fulmea.attacking = false;
    }
}
