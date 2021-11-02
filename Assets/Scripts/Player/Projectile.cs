using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	Rigidbody2D rigidbody2d;
	    
	void Awake()
	{
		rigidbody2d = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		if(transform.position.magnitude > 1000.0f)
        	Destroy(gameObject);
	}
	
    public void Launch(Vector2 direction, float force)
	{
   		rigidbody2d.AddForce(direction * force);
	}

	void OnCollisionEnter2D(Collision2D other)
	{
	    if(other.collider.transform.parent != null)
	    {
	    	FulmeaController fulmea = other.collider.transform.parent.GetComponent<FulmeaController>();
    		if (fulmea != null) fulmea.ChangeHealth(-20);
    	}
  		Destroy(gameObject);
	}
}
