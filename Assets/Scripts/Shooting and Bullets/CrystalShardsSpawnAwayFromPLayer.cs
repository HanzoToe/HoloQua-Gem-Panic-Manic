using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalShardsSpawnAwayFromPLayer : MonoBehaviour
{

    public Transform playerTransform; // Reference to the player's transform
    public float rotationSpeed = 5f; // Speed at which the object rotates

    private void Start()
    {
        // Find the "Player" GameObject in the scene
        GameObject playerGameObject = GameObject.FindGameObjectWithTag("Player");

        // Check if the player GameObject exists
        if (playerGameObject != null)
        {
            // Get the Transform component of the player GameObject
            playerTransform = playerGameObject.transform;
        }
        else
        {
            Debug.LogError("Player GameObject not found!"); // Log an error if the player GameObject is not found
        }
    }



    // Update is called once per frame
    void Update()
    {

        if (playerTransform != null)
        {
            // Calculate the direction from this object to the player
            Vector3 direction = playerTransform.transform.position - transform.position;

            // Log the direction vector
            Debug.DrawRay(transform.position, direction, Color.green);

            // Calculate the angle needed to rotate to face the player
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;


            angle += 180f;


            // Rotate this object to face the player
            Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);


            // Debug statement to check the angle
            Debug.Log("Angle to player: " + angle);

            // Debug statement to check the positions of the object and the player
            Debug.Log("Object position: " + transform.position);
            Debug.Log("Player position: " + playerTransform.transform.position);
        }
        else
        {
            Debug.LogWarning("Player reference is not set!"); // Log a warning if the player reference is not set
        }
    }
}

