using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullets : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    public float BulletSpeed = 12f;


    // Start is called before the first frame update
    void Start()
    {
  
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("PlayerGameObject");

        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * BulletSpeed;

        float rot = Mathf.Atan2(-direction.y, -direction.x)* Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0,0,rot + 90); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerGameObject"))
        {
            Destroy(gameObject);
            Player_hp.hp -= 1;
        }

        if (collision.CompareTag("Pillar"))
        {
            PillarHP pillarHP = collision.GetComponent<PillarHP>();

            if (pillarHP != null)
            {
                pillarHP.Pillarhp -= 1;

            }

            Destroy(gameObject);
        }
    }
}
