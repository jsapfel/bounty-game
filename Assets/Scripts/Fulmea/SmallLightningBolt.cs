using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallLightningBolt : MonoBehaviour
{
    SpriteRenderer render;
    Color color;
    float duration = 0.4f;
    float timer;

    void Start()
    {
        render = GetComponent<SpriteRenderer>();
        color = render.color;

        Collider2D hit = Physics2D.OverlapCircle((Vector2)transform.position, 0.25f, LayerMask.GetMask("Player"));
        if(hit != null) hit.GetComponent<RubyController>().ChangeHealth(-15);
        Destroy(gameObject, duration);
    }

    // void Update()
    // {
    //     if(timer < duration)
    //     {
    //         color.a = 1 - timer/duration;
    //         render.color = color;
    //         timer += Time.deltaTime;
    //     }
    // }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere((Vector2)transform.position, 0.25f);
    }
}