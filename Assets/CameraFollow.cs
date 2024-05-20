using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float FollowSpeed = 2f;
    public float yOffest = 1f;
    public Transform Target; 


    // Update is called once per frame
    void Update()
    {
        Vector3 newpos = new Vector3(Target.position.x, Target.position.y + yOffest, -10f);
        transform.position = Vector3.Slerp(transform.position, newpos, FollowSpeed * Time.deltaTime);
    }
}
