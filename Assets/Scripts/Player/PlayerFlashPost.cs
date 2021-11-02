using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PlayerFlashPost : MonoBehaviour
{
	public static PlayerFlashPost instance {get; private set;}
	public PostProcessVolume volume;

    float timer;
    float flashTime = -1;
    float startingWeight;

    void Awake()
    {
    	instance = this;
    }

    void Update()
    {
        if(timer < flashTime)
        {
            timer += Time.deltaTime;
            volume.weight = Mathf.Lerp(startingWeight, 0f, timer/flashTime);
        }
    }

    public void Flash(float weight, float time)
    {
        volume.weight = weight;
        startingWeight = weight;
        flashTime = time;
        timer = 0;
    }
}
