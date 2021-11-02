using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
	public float alphaDuration = 0.2f;
	public float swingDuration = 0.1f;

	SpriteRenderer render;
	Color color;
	float timer;
	Quaternion firstTarget;
	Quaternion secondTarget;
	Vector2 attackCenter;
    Quaternion initRotation;
    bool swing = true;

    void Start()
    {
   		render = GetComponent<SpriteRenderer>();
   		color = render.color;

        float angle;
        Vector3 axis;
        initRotation = transform.rotation;
   		transform.rotation.ToAngleAxis(out angle, out axis);
        if(axis == Vector3.right) axis = Vector3.forward;
   		firstTarget = Quaternion.AngleAxis(angle - 45f, axis);
   		secondTarget = Quaternion.AngleAxis(angle + 45f, axis);
    }

    void Update()
    {
    	timer += Time.deltaTime;

    	if(timer < alphaDuration)
    	{
        	color.a = timer / alphaDuration;
        	render.color = color;
        	transform.rotation = Quaternion.RotateTowards(transform.rotation, firstTarget, 45/alphaDuration * Time.deltaTime);

            transform.localPosition = transform.localPosition + 0.4f * Time.deltaTime / alphaDuration * Vector3.up;
        }
    	else if(timer < alphaDuration + swingDuration)
        {
        	transform.rotation = Quaternion.RotateTowards(transform.rotation, secondTarget, 90/swingDuration * Time.deltaTime);
        	if(swing)
        	{
                attackCenter = transform.position + initRotation * Vector3.right * 0.7f;
        		Collider2D collider = Physics2D.OverlapCircle(attackCenter, 0.7f, LayerMask.GetMask("EnemyHitbox"));
        		if(collider) collider.transform.parent.GetComponent<FulmeaController>().ChangeHealth(-20);
        		swing = false;
        	}
        }
        else  Destroy(gameObject);
    }

    void OnDrawGizmos()
    {
    	Gizmos.color = Color.yellow;
    	Gizmos.DrawWireSphere(attackCenter, 0.7f);
    }
}
