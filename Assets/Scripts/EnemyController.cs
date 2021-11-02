using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Animator animator;
	Rigidbody2D rigidbody2d;
	public float speed = 2.0f;
    public int direction = 1;

	public bool vertical;
	public float changeTime = 2.0f;
	float timer;

    bool broken = true;
    public ParticleSystem smokeEffect;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    	rigidbody2d = GetComponent<Rigidbody2D>();
        timer = changeTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (!broken) return;

        timer -= Time.deltaTime;
        if (timer < 0)
        {
            direction *= -1;
            timer = changeTime;
        }
    }

    void FixedUpdate()
    {
        if (!broken) return;
        
        Vector2 position = rigidbody2d.position;
        if (vertical)
        {
        	position.y += Time.deltaTime * speed * direction;
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);
        }
        else
        {
        	position.x += Time.deltaTime * speed * direction;
            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0);
        }
        rigidbody2d.MovePosition(position);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        RubyController controller = other.gameObject.GetComponent<RubyController>();
        if (controller != null)
            controller.ChangeHealth(-1);
    }

    public void Fix()
    {
        broken = false;
        rigidbody2d.simulated = false;
        Destroy(transform.GetChild(0).gameObject);
        smokeEffect.Stop();
    }
}
