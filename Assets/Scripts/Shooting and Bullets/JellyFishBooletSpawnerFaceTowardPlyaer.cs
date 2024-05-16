using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyFishBooletSpawnerFaceTowardPlyaer : MonoBehaviour
{
    public Transform playertransform;
    public float rotationspeed; 

    // Start is called before the first frame update
    void Start()
    {
        GameObject playergameobject = GameObject.FindGameObjectWithTag("Player"); 

        if(playergameobject != null)
        {
            playertransform = playergameobject.transform;
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        if (playertransform != null)
        {
            Vector3 direction = playertransform.position - transform.position;

            // Log the direction vector
            Debug.DrawRay(transform.position, direction, Color.green);

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            Quaternion targetrotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetrotation, rotationspeed * Time.deltaTime);
            // Debug statement to check the angle
            Debug.Log("Angle to player: " + angle);

            // Debug statement to check the positions of the object and the player
            Debug.Log("Object position: " + transform.position);
            Debug.Log("Player position: " + playertransform.transform.position);
        }
        else
        {
            Debug.LogWarning("Player reference is not set!"); // Log a warning if the player reference is not set
        }
    }

}


