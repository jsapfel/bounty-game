using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningPulse : MonoBehaviour
{
	// public float duration = 0.25f;

	// float timer;
	// float rotSpeed;
	// float angle;

	public ParticleSystem ps;

	bool hit;

	// 	angle = Random.Range(0f, 360f);
	// 	rotSpeed = Random.Range(720f, 1440f);
	// 	rotSpeed *= (Random.value < 0.5f) ? 1 : -1;
	// 	transform.rotation = Quaternion.Euler(0, 0, angle);
	// }
	// void Update()
	// {
	// 	if(timer < duration)
	// 	{
	// 		timer += Time.deltaTime;
	// 		transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, timer/duration);
	// 		angle += rotSpeed * Time.deltaTime;
	// 		transform.rotation = Quaternion.Euler(0, 0, angle);
	// 	}	
 //        else Destroy(gameObject);
	// }
	
	void OnParticleCollision(GameObject other)
	{
		if(!hit)
        {
	    	RubyController ruby = other.GetComponent<RubyController>();
	        if (ruby != null)
	        {
        		hit = true;
	        	var coll = ps.collision;
	        	coll.colliderForce = 0;
	        	ruby.ChangeHealth(-50);
	        }
        }
	}

	// void OnCollisionEnter2D(Collision2D other)
	// {
	//     if(other != null)
	//     {
	//     	RubyController ruby = other.transform.GetComponent<RubyController>();
 //    		if (ruby != null) ruby.ChangeHealth(-33);
 //    	}
 //  	}
}

