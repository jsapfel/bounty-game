using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBolt : MonoBehaviour
{
    float duration = 0.6f;

    void Start()
    {
        PlayerFlashPost.instance.Flash(0.3f, 5/4f * duration);
        CameraShake.instance.Shake(0.7f, duration);
        Collider2D spikeCheck = Physics2D.OverlapCircle((Vector2)transform.position, 2f, LayerMask.GetMask("Default"));
        if(spikeCheck && spikeCheck.CompareTag("Spike"))
        {
            transform.position = spikeCheck.transform.position;
            Destroy(spikeCheck.gameObject);
        }
        
        Collider2D hit = Physics2D.OverlapCircle((Vector2)transform.position, 0.5f, LayerMask.GetMask("Enemy"));
        if(hit != null) hit.GetComponent<FulmeaController>().ChangeHealth(-30);
        Destroy(gameObject, duration);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere((Vector2)transform.position, 0.5f);
        Gizmos.DrawWireSphere((Vector2)transform.position, 2f);
    }
}
