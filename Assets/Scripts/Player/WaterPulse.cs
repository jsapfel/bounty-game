using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPulse : MonoBehaviour
{
	public ParticleSystem ps;
	bool hit;
	
	void OnParticleCollision(GameObject other)
	{
		if(!hit)
        {
	    	FulmeaController enemy = other.GetComponent<FulmeaController>();
	        if (enemy != null)
	        {
        		hit = true;
	        	var coll = ps.collision;
	        	coll.colliderForce = 0;
	        }
        }
	}
}

