using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Variables")]
    [SerializeField] private float BulletSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  
        Vector2 direction = new Vector2(Shooting.Mouseposition.x - transform.position.x, Shooting.Mouseposition.y - transform.position.y).normalized;
        rb.velocity = direction * BulletSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
