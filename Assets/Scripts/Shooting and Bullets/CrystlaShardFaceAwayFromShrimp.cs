using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystlaShardFaceAwayFromShrimp : MonoBehaviour
{
    private GameObject nearestshrimp; 
    public float rotationspeed = 5f;
    public float Range = 5f; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, Range);

        foreach(Collider2D collider in colliders)
        {
            if (collider.CompareTag("Shrimp"))
            {
                if (nearestshrimp == null || Vector2.Distance(transform.position, collider.transform.position) < Vector2.Distance(transform.position, nearestshrimp.transform.position))
                {
                    nearestshrimp = collider.gameObject; 
               }
                
            }
        }


        if(nearestshrimp != null)
        {
            // Calculate the direction from this object to the player
            Vector3 direction = nearestshrimp.transform.position - transform.position;

            // Log the direction vector
            Debug.DrawRay(transform.position, direction, Color.green);

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            angle += 180; 

            Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation =  Quaternion.Slerp(transform.rotation, targetRotation, rotationspeed * Time.deltaTime);
            
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, Range);
    }

}
