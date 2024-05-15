using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OctoPus_Logic: MonoBehaviour
{
    private Rigidbody2D rb;
    public GameObject player;
    public GameObject bulletprefab; 
    public float movespeed = 5f;
    public float viewrange = 5f;
    public float SprayTimer = 3f;
    public float SprayCooldown = 5f; 



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("PlayerGameObject");

        float distance = Vector2.Distance(player.transform.position, transform.position); 

        if(distance <= viewrange)
        {
            StartBlasting(); 
        }
    }

    private void StartBlasting()
    {
       Instantiate(bulletprefab, gameObject.transform.position, Quaternion.identity);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, viewrange);
    }
}
