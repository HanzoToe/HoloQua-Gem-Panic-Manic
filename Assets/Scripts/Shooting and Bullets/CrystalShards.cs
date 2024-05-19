using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalShards : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Variables")]
    [SerializeField] private float BulletSpeed = 5f;
    [SerializeField] private int Damage = 1;


    public Transform PlayerTransform;
    public LayerMask Enemy; 


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Vector2 direction = transform.right; 
        rb.velocity = direction.normalized * BulletSpeed;
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

        Destroy(gameObject);
    }
}
