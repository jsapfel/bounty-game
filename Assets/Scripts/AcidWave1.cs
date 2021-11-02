using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidWave : MonoBehaviour
{
	void Update()
	{
		if(transform.localScale.x < 1.66f) transform.localScale += 0.075f*Vector3.right;
        else Destroy(transform.parent.gameObject);
	}
	

	void OnCollisionEnter2D(Collision2D other)
	{
	    if(other.collider.transform.parent != null)
	    {
	    	EnemyController e = other.collider.transform.parent.GetComponent<EnemyController>();
    		if (e != null) e.Fix();
    	}
  	}
}
