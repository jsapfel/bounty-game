using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpandSystem : MonoBehaviour
{
	public ParticleSystem ps;
	ParticleSystem.ShapeModule psShape;
	ParticleSystem.EmissionModule psEmission;

    CircleCollider2D myCollider;

    void Start()
    {
        //ps = GetComponent<ParticleSystem>();
        psShape = ps.shape;
        psEmission = ps.emission;
        myCollider = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
    	if(psShape.radius < 2)
    	{
    		psShape.radius += 8f*Time.deltaTime;
       		psEmission.rateOverTime = 40*psShape.radius;

    	}
    	else Destroy(gameObject);
    }

    void FixedUpdate()
    {
        myCollider.radius += 8f*Time.deltaTime;
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
