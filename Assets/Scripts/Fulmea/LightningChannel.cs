using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningChannel : MonoBehaviour
{
	Animator userAnimator;

    void Start()
    {
        userAnimator = transform.parent.GetComponent<Animator>();
    }

    void Update()
    {
        transform.rotation = Quaternion.Euler(Mathf.Atan2(-userAnimator.GetFloat("LookY"), userAnimator.GetFloat("LookX")) * Mathf.Rad2Deg, 90, 0);
    }

    void OnParticleCollision(GameObject other)
    {
        RubyController ruby = other.GetComponent<RubyController>();
        if (ruby != null) ruby.ChangeHealth(-1);
    }
}
