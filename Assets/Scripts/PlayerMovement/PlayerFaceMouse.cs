using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFaceMouse : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = (Shooting.Mouseposition - (Vector2)transform.position).normalized;

        Vector2 scale = transform.localScale;
        if (direction.x < 0)
        {
            scale.x = -1f;
        }
        else if (direction.x > 0)
        {
            scale.x = 1f;
        }
        transform.localScale = scale;
    }
}
