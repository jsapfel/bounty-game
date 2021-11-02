using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storm : MonoBehaviour
{
    public GameObject smallLightingBoltPrefab;
    public float radius = 2.05f;
    //public Image flashImage;
    
	float boltTimer = -1f;
    //float timer;
    //Color flashColor;
    //bool strike;

    void Start()
    {
        Destroy(gameObject, 5f);
    }

    void Update()
    {
        // if(strike)
        // {
        //     timer += Time.deltaTime;
        //     if(timer < 0.05f)
        //         flashColor.a = timer / (0.05f * 6);
        //     else if(timer < 0.12f)
        //         flashColor.a = (0.12f - timer) / (0.07f * 6);
        //     else 
        //     {
        //         flashColor.a = 0f;
        //         strike = false;
        //     }
        //     flashImage.color = flashColor;
        // }

    	boltTimer -= Time.deltaTime;
    	if(boltTimer < 0)
    	{
            //strike = true;
            //timer = 0f;

            LightningFlash.instance.Flash(0.2f, 0.15f);
            CameraShake.instance.Shake(0.3f, 0.12f);

            Collider2D spikeCheck = Physics2D.OverlapCircle((Vector2)transform.position, radius, LayerMask.GetMask("Default"));
            if(spikeCheck && spikeCheck.CompareTag("Spike"))
            {
                Instantiate(smallLightingBoltPrefab, spikeCheck.transform.position, Quaternion.identity);
                Destroy(spikeCheck.gameObject);
            }

            else
            {
                int boltCount = Random.Range(1, 5);
        		for(int i = 0; i < boltCount; i++){
        			float dist = Random.Range(0.15f, radius);
    		        float angle = Random.Range(0f, 2*Mathf.PI);
    		        Vector3 dir = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0);
    		        Instantiate(smallLightingBoltPrefab, transform.position+dist*dir, Quaternion.identity);
        		}
            }

            boltTimer = Random.Range(0.12f, 0.4f);
        }
    }
}
