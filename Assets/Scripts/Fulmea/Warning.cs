using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warning : MonoBehaviour
{
    public GameObject attack;

    Transform player;
    Vector3 offset = 0.2f * Vector3.up; 
    float speed = 1f;

    void Start()
    {
        player = transform.parent;
        transform.parent = null;
    }

    void Update()
    {
        if(player)
            transform.position = Vector3.MoveTowards(transform.position, player.position + offset, speed * Time.deltaTime);
    }

    void OnDestroy()
    {
        Instantiate(attack, transform.position, Quaternion.identity);
    }
}



// {
// 	float timer;
// 	GameObject attack;
//     bool channel;
//     GameObject user;

//     // Update is called once per frame
//     void Update()
//     {
//         timer -= Time.deltaTime;
//         if(timer < 0)
//         {
//             if(!channel)
//         	   Instantiate(attack, transform.position, Quaternion.identity);
//             else
//             {
//                 Vector3 userLocation = user.transform.position;
//                 Vector3 targetLocation = transform.position;
//                 targetLocation.z = userLocation.z;
//                 Vector3 vectorToTarget = targetLocation - userLocation;
//                 float angleToTarget = Mathf.Atan2(-vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
//                 GameObject lightningChannel = Instantiate(attack, user.transform.position + 0.6f*Vector3.up, Quaternion.Euler(angleToTarget,90,0));
//                 lightningChannel.transform.parent = user.transform;
//             }
//             Destroy(gameObject);
//         }
//     }

//     public void SetVariables(float timerP, GameObject attackP, bool channelP, GameObject userP)
//     {
//         timer = timerP;
//         attack = attackP;
//         channel = channelP;
//         user = userP;
//     }
// }
