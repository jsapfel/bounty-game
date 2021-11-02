using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBolt : MonoBehaviour
{
    //public FulmeaController fulmea;

    // Image flashImage;
    // Color flashColor;
    // float timer;

    SpriteRenderer render;
    Color color;
    float duration = 0.6f;
    float timer;

    void Start()
    {
        // flashImage = fulmea.lightningFlash;
        // flashColor = flashImage.color;

        render = GetComponent<SpriteRenderer>();
        color = render.color;

        LightningFlash.instance.Flash(0.25f, 5/4f * duration);
        CameraShake.instance.Shake(0.6f, duration);
        Collider2D spikeCheck = Physics2D.OverlapCircle((Vector2)transform.position, 2f, LayerMask.GetMask("Default"));
        if(spikeCheck && spikeCheck.CompareTag("Spike"))
        {
            transform.position = spikeCheck.transform.position;
            Destroy(spikeCheck.gameObject);
        }
        
        Collider2D hit = Physics2D.OverlapCircle((Vector2)transform.position, 0.5f, LayerMask.GetMask("Player"));
        if(hit != null) hit.GetComponent<RubyController>().ChangeHealth(-30);
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

    //     // if(timer < 0.08f)
    //     //     flashColor.a = timer / (0.08f * 5);
    //     // else if(timer < 0.2f)
    //     //     flashColor.a = (0.2f - timer) / (0.12f * 5);
    //     // else 
    //     // {
    //     //     flashColor.a = 0f;
    //     //     flashImage.color = flashColor;
    //     //     Destroy(gameObject);
    //     // }
    //     // flashImage.color = flashColor;
    // }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere((Vector2)transform.position, 0.5f);
        Gizmos.DrawWireSphere((Vector2)transform.position, 2f);
    }
}
