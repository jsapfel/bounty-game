using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{

	public static CameraShake instance {get; private set;}
	public CinemachineVirtualCamera virtualCamera;

	CinemachineBasicMultiChannelPerlin cameraMCP;
	float timer;
	float shakeTime = -1;
	float startingIntensity;

	void Awake()
	{
		instance = this;

	}
    // Start is called before the first frame update
    void Start()
    {
    	cameraMCP = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();   
    }

    // Update is called once per frame
    void Update()
    {
    	if(timer < shakeTime)
    	{
    		timer += Time.deltaTime;
    		cameraMCP.m_AmplitudeGain = Mathf.Lerp(startingIntensity, 0f, timer/shakeTime);
    	}
    	   
    }

    public void Shake(float intensity, float time)
    {
    	cameraMCP.m_AmplitudeGain = intensity;
    	startingIntensity = intensity;
    	shakeTime = time;
    	timer = 0;
    }
}
