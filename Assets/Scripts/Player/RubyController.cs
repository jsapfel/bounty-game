using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    //public GameObject projectilePrefab;
    public GameObject swordPrefab;
    public GameObject spikePrefab;
    public GameObject groundBreakPrefab;
    public GameObject healPrefab;
    public GameObject waterPulsePrefab;
    public GameObject lightningBoltPrefab;
    public GameObject acidWavePrefab;

    public UIElementCooldown[] cooldowns;
    public GameObject lightningElem;
    public GameObject airElem;

    public AudioClip shootClip;
    public AudioClip hitClip;

    public float speed = 4.0f;
    public int maxHealth = 100;
    public float timeInvincible = 2.0f;
    public int health {get{return currentHealth;}}

    Camera mainCamera;
    Rigidbody2D rb;
    Animator animator;
    AudioSource audioSource;

    Vector2 move;
    Vector2 lookDirection = new Vector2(1,0);
    int currentHealth;
    bool isInvincible;
    float invincibleTimer;

    int elementIndex = 0;
    int availableElementCount = 2;

    void Start()
    {
        mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

        currentHealth = maxHealth;
        cooldowns[elementIndex].Select();
    }

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Alpha0) && availableElementCount < 4)
        {
            if(availableElementCount == 2) lightningElem.SetActive(true);
            else if(availableElementCount == 3) airElem.SetActive(true);
            availableElementCount++;
        }

        move.Set(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if(move.magnitude > 1)
        {
            move.Normalize();
            lookDirection = move;
        }
        else if(!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
            lookDirection = move.normalized;

        animator.SetFloat("LookX", lookDirection.x);
        animator.SetFloat("LookY", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }

        // if (Input.GetKeyDown(KeyCode.X))
        // { 
        //     RaycastHit2D hit = Physics2D.Raycast(rb.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
        //     if (hit.collider != null)
        //     {
        //         NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>();
        //         if (character != null)
        //             character.DisplayDialog();
        //     }
        // }

        // if(Input.GetKeyDown(KeyCode.C))
        // {
        //     audioSource.PlayOneShot(shootClip);
        //     GameObject projectileObject = Instantiate(projectilePrefab, rb.position + Vector2.up * 0.4f, Quaternion.identity);
        //     Projectile projectile = projectileObject.GetComponent<Projectile>();
        //     projectile.Launch(lookDirection, 300);
        //     //animator.SetTrigger("Launch");
        // }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if(Mathf.Abs(scroll) > 0.01) setElementIndex(scroll);
        if(cooldowns[elementIndex].usable)
        {
            if(Input.GetButtonUp("Fire1"))
            {
                if(elementIndex == 0) UseMetal1();
                else if(elementIndex == 1) UseWater1();
                else if(elementIndex == 2) UseLightning1();
                else if(elementIndex == 3) UseAir1();
            }
            else if(Input.GetButtonUp("Fire2"))
            {
                if(elementIndex == 0) UseMetal2();
                else if(elementIndex == 1) UseWater2();
                else if(elementIndex == 2) UseLightning2();
                else if(elementIndex == 3) UseAir2();
            }
        }
        if(Input.GetKeyUp(KeyCode.Alpha1)) UseAcidWave();
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + move * (speed * Time.deltaTime));
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if(isInvincible) return;
            audioSource.PlayOneShot(hitClip);
            //isInvincible = true;
            //invincibleTimer = timeInvincible;
            //animator.SetTrigger("Hit");
        }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    void UseMetal1()
    {
        cooldowns[0].UseElement(1f);
        Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg);
        Instantiate(swordPrefab, transform.position, rotation, transform);
    }

    void UseMetal2()
    {
        cooldowns[0].UseElement(2f);
        Vector3 location = transform.position + 0.7f * (Vector3)lookDirection;
        Instantiate(groundBreakPrefab, location-0.1f*Vector3.up, Quaternion.identity);
        Instantiate(spikePrefab, location - 0.8f*Vector3.up - Vector3.forward, Quaternion.identity);
    }

    void UseWater1()
    {
        cooldowns[1].UseElement(3f);
        ChangeHealth(20);
        Instantiate(healPrefab, transform.position + 0.45f*Vector3.up, Quaternion.identity, transform);
    }

    void UseWater2(){
        cooldowns[1].UseElement(2f);
        Instantiate(waterPulsePrefab, transform.position + 0.4f*Vector3.up, Quaternion.identity);
    }

    void UseLightning1()
    {
        cooldowns[2].UseElement(4f);
        Vector2 position = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, mainCamera.nearClipPlane));
        Instantiate(lightningBoltPrefab, position, Quaternion.identity);
    }

    void UseAir1()
    {
        cooldowns[3].UseElement(3f);
        int layerMask = LayerMask.GetMask("Default","Enemy");
        float dashDistance = 2f;
        Vector3 direction = unitToCursor();
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, direction, dashDistance, layerMask);

        Collider2D endCollider = Physics2D.OverlapPoint(transform.position + direction * dashDistance, LayerMask.GetMask("Default"));
        System.Array.Sort(hits, (x,y) => x.distance.CompareTo(y.distance));

        for (int i = 0; i < hits.Length; i++)
        {
            FulmeaController enemy = hits[i].collider.GetComponent<FulmeaController>();
            if (enemy != null) enemy.ChangeHealth(-20);
        }
        if (hits.Length > 0 && endCollider != null)
            transform.position = (Vector3)hits[hits.Length-1].point - 0.1f*direction + transform.position.z*Vector3.forward;
        else transform.position = transform.position + direction * dashDistance;
    }

    void UseLightning2() {}

    void UseAir2() {}

    void UseAcidWave()
    {
        if(!cooldowns[0].usable || !cooldowns[1].usable) return;
        cooldowns[0].UseElement(4f);
        cooldowns[1].UseElement(4f);
        Vector2 toCursor = unitToCursor();
        Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(toCursor.y, toCursor.x) * Mathf.Rad2Deg);
        Instantiate(acidWavePrefab, rb.position + Vector2.up * 0.4f, rotation);
    }

    void setElementIndex(float scroll)
    {
        cooldowns[elementIndex].Deselect();
        if (scroll > 0)
            elementIndex = (elementIndex + 1) % availableElementCount;
        else
        {
            elementIndex--;
            if(elementIndex < 0) elementIndex = availableElementCount - 1;
        }
        cooldowns[elementIndex].Select();
    }

    RaycastHit2D getCursorCollision(string[] layers)
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        return Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, LayerMask.GetMask(layers));
    }
    RaycastHit2D getCursorCollision(string layer)
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        return Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, LayerMask.GetMask(layer));
    }

    Vector2 unitToCursor()
    {
        Ray mouse = mainCamera.ScreenPointToRay(Input.mousePosition);
        Vector2 direction = (Vector2)mouse.origin-rb.position;
        return direction.normalized;
    }

}
