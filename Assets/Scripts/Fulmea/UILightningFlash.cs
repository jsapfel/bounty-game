using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILightningFlash : MonoBehaviour
{
	public static UILightningFlash instance {get; private set;}
	public Image flashImage;

    Color color;
    bool flashing;
    float timer;
    float flashTime;

    void Awake()
    {
    	instance = this;
    }

    void Start()
    {
    	color = flashImage.color;
    }

    void Update()
    {
    	if(flashing)
    	{
    		timer += Time.deltaTime;
	    	if(timer < 0.3f * flashTime)
	            color.a = timer / (0.3f * flashTime * 4);
	        else if(timer < flashTime)
	            color.a = (flashTime - timer) / (0.7f * flashTime * 4);
	        else
	        {
	        	color.a = 0f;
	        	flashing = false;
	        	timer = 0;
	        }
	        flashImage.color = color;
	    }
    }

    public void Flash(float time)
    {
    	flashing = true;
    	flashTime = time;
    }

    // public void SetValue(float value)
    // {				      
    //     mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * value);
    // }
}
