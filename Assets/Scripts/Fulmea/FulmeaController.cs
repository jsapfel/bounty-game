using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FulmeaController : MonoBehaviour
{
    public Camera mainCamera;

    public int maxHealth;
    public Slider healthBar;
    public bool isInvincible;
    public float rotSpeed = 20;

    public float minAttackCooldown;
    public float maxAttackCooldown;
    public bool attacking = true;
    public float attackCooldown = -1;
    public Image lightningFlash;

    Coroutine introAttack;
    int currentHealth;
    Animator animator;
    Vector2 lookDirection;
    Transform player;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player").transform;
        lookDirection = (player.position - transform.position).normalized;
    }

    void Update()
    {
        if(!attacking)
        {
            Vector2 dirToPlayer = (player.position - transform.position).normalized;
            lookDirection = Vector3.RotateTowards(lookDirection, dirToPlayer, rotSpeed * Time.deltaTime, 0f);
            animator.SetFloat("LookX", lookDirection.x);
            animator.SetFloat("LookY", lookDirection.y);

            if(attackCooldown < 0)
            {
                attacking = true;
                attackCooldown = Random.Range(minAttackCooldown, maxAttackCooldown);
                animator.SetBool("Warning", true);
            }
            else attackCooldown -= Time.deltaTime;
        }
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if(isInvincible) return;
            //animator.SetTrigger("Hit");
        }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        if(currentHealth <= maxHealth/2) animator.SetBool("Stage2", true);
        if(currentHealth <= 0)
        {
            animator.SetTrigger("Die");
            isInvincible = true;
        }
        healthBar.value = currentHealth/(float)maxHealth;
    }

    IEnumerator ChangeHealthOverTime(int amount, float interval, float duration)
    {
        for(float i = 0f; i < duration; i += interval)
        {
            ChangeHealth(amount);
            yield return new WaitForSeconds(interval);
        }
    }

    void OnParticleCollision(GameObject other)
    {
        if(other.CompareTag("AcidWave"))
        {
            ChangeHealth(-20);
            StartCoroutine(ChangeHealthOverTime(-2, 0.5f, 5f));
        }
    }

    IEnumerator IntroAttack(GameObject warning)
    {
        while(true)
        {
            //Vector2 location = mainCamera.ViewportToWorldPoint(new Vector2(Random.value, Random.value));
            GameObject newWarning = Instantiate(warning, player.position, Quaternion.identity);
            Destroy(newWarning, 0.5f);
            yield return new WaitForSeconds(0.25f);
        }
    }
    public void StartIntroAttackCoroutine(GameObject warning) { introAttack = StartCoroutine(IntroAttack(warning)); }
    public void StopIntroAttackCoroutine() { StopCoroutine(introAttack); }
}
