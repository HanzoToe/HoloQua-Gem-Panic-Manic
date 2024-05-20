using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceMouse : MonoBehaviour
{
    public Vector2 Pointerposition { get; set; }

    private void Update()
    {

        Vector2 direction = (Pointerposition - (Vector2)transform.position).normalized;
        transform.right = direction.normalized;

        Vector2 scale = transform.localScale;
        if(direction.x < 0)
        {
            scale.y = -1f; 
        }else if (direction.x > 0)
        {
            scale.y = 1f;
        }
        transform.localScale = scale;
    }
}
