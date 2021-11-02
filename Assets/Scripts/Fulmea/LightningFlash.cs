using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class LightningFlash : MonoBehaviour
{
	public static LightningFlash instance {get; private set;}
	public PostProcessVolume volume;

    // bool flashing;
    float timer;
    float flashTime = -1;
    float startingWeight;

    void Awake()
    {
    	instance = this;
    }

    void Update()
    {

    	// if(flashing)
    	// {
    	// 	timer += Time.deltaTime;
	    // 	if(timer < 0.3f * flashTime)
	    //         volume.weight = 0.235f * timer / (0.3f * flashTime);
	    //     else if(timer < flashTime)
	    //         volume.weight = 0.235f * (flashTime - timer) / (0.7f * flashTime);
	    //     else
	    //     {
     //            volume.weight = 0;
	    //     	flashing = false;
	    //     	timer = 0;
	    //     }
	    // }
        if(timer < flashTime)
        {
            timer += Time.deltaTime;
            volume.weight = Mathf.Lerp(startingWeight, 0f, timer/flashTime);
        }
    }

    public void Flash(float weight, float time)
    {
        // flashing = true;
        volume.weight = weight;
        startingWeight = weight;
        flashTime = time;
        timer = 0;
    }

    // public void SetValue(float value)
    // {				      
    //     mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * value);
    // }
}
