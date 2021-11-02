using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class UIElementCooldown : MonoBehaviour
{
    public RectTransform bg;
    public RectTransform mask;
    public bool usable = true;
   
    float cooldown;
    float timer;
    float originalSize;

    void Start()
    {
        originalSize = mask.rect.height;
    }

    void Update()
    {
        if(timer < cooldown)
        {
            timer += Time.deltaTime;
            mask.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, originalSize * timer / cooldown);
        }
        else usable = true;
    }

    public void UseElement(float newCooldown)
    {
        mask.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0);
        usable = false;
        timer = 0;
        cooldown = newCooldown;
    }

    public void Select()
    {
        bg.localScale = new Vector3(1.2f,1.2f,1);
    }

    public void Deselect()
    {
        bg.localScale = new Vector3(1,1,1);
    }
}

