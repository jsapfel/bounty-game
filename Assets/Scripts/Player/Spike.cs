using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
	public float lifetime;
	public Transform mask;
	public float createTime;
	
	Collider2D myCollider;
	Vector3 startPos;
	float timer;

	void Start()
	{
		myCollider = GetComponent<Collider2D>();
		startPos = transform.position;

		Destroy(gameObject, lifetime);

		Collider2D collider = Physics2D.OverlapCircle(transform.position + 1.07f * Vector3.up, 0.28f, LayerMask.GetMask("Enemy"));
        if(collider) collider.GetComponent<FulmeaController>().ChangeHealth(-20);
	}

	void Update()
	{
		if(timer < createTime)
		{
			timer += Time.deltaTime;
			float offsetY = 0.8f * timer / createTime;
			transform.position = startPos + offsetY * Vector3.up;
			mask.localScale = new Vector3(1f, 0.2f + offsetY, 1f);
			if(timer > 2 * createTime / 3) myCollider.enabled = true;
		}
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(new Vector3(transform.position.x, transform.position.y+1.07f, 0f), 0.28f);
	}
	

	// void OnCollisionEnter2D(Collision2D other)
	// {
	//     if(other.collider.transform.parent != null)
	//     {
	//     	EnemyController e = other.collider.transform.parent.GetComponent<EnemyController>();
 //    		if (e != null) e.Fix();
 //    	}
 //  	}
}
