using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrimp_Logic : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] private float MovementSpeed = 5f;
    [SerializeField] private float ViewDistance = 5f;
    [SerializeField] Rigidbody2D rb;
    

    public GameObject player;
    public Vector2 Moving;
    public int TestDamage = 0; 


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("PlayerGameObject");
        rb = GetComponent<Rigidbody2D>();

        float distance = Vector2.Distance(transform.position, player.transform.position);

        if(distance <= ViewDistance)
        {
            MoveTowardsPlayer();
        }
        else if(distance > ViewDistance)
        {
            rb.velocity = Vector2.zero;
        }

        Debug.Log(TestDamage);
    }

    private void MoveTowardsPlayer()
    {
        Moving = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y);
        rb.velocity = Moving.normalized * MovementSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("PlayerGameObject"))
        {
            TestDamage += 1;
            Destroy(gameObject); 
        }

        if (collision.collider.CompareTag("Pillar"))
        {
            PillarHP pillarHP = collision.collider.GetComponent<PillarHP>();

            if (pillarHP != null)
            {
                pillarHP.Pillarhp = 0;
                pillarHP.SetDestroyedByShrimp();
            }

            Destroy(gameObject);
        }
    }



    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, ViewDistance);
    }
}
