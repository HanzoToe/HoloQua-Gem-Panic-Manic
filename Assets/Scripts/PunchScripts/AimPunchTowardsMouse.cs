using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimPunchTowardsMouse : MonoBehaviour
{
    public Vector2 Pointerposition { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 direction = (Pointerposition - (Vector2)transform.position).normalized;
        transform.right = direction;

        Vector2 scale = transform.localScale; 
        if(direction.x < 0)
        {
            scale.y = -1f;
        }
        else if(direction.x > 0)
        {
            scale.y = 1f; 
        }
        transform.localScale = scale; 
    }
}
