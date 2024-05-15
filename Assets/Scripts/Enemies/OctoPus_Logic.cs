using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OctoPus_Logic: MonoBehaviour
{
    private Rigidbody2D rb;
    public GameObject player;
    public GameObject bulletprefab; 
    public float movespeed = 3f;
    public float viewrange = 5f;
    public float SprayTimer = 0f;
    bool AllowedToBlast = true;
    Vector2 moving; 


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

        if(distance <= viewrange && AllowedToBlast)
        {
            StartCoroutine(StartBlasting()); 
        }

        if (SprayTimer >= 3f)
        {
            StopCoroutine(StartBlasting());
            AllowedToBlast = false;
        }

        if(!AllowedToBlast)
        {
            SprayTimer -= Time.deltaTime;
            StartCoroutine(RunAway());
        }

        if(SprayTimer <= 0f)
        {
            AllowedToBlast = true;
            rb.velocity = Vector3.zero;
            StopCoroutine(RunAway());
        }
    }

    IEnumerator StartBlasting()
    {
       Instantiate(bulletprefab, gameObject.transform.position, Quaternion.identity);
        yield return null;

        SprayTimer += Time.deltaTime;
    }

    IEnumerator RunAway()
    {
        moving = new Vector2(transform.position.x - player.transform.position.x, transform.position.y - player.transform.position.y);
        rb.velocity = moving.normalized * movespeed; 
        yield return null;
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, viewrange);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("PlayerGameObject"))
        {
            rb.isKinematic = true;
            Debug.Log("Player take damage");
        }
    }
}
