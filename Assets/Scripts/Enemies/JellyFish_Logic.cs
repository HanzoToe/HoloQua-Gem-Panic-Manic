using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyFish_Logic : MonoBehaviour
{

    public int JellHp = 6; 

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (JellHp <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
      


        if (collision.collider.CompareTag("PlayerGameObject"))
        {
            Player_hp.hp -= 1;
        }
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            JellHp -= 1;
        }

        if (collision.CompareTag("Crystal"))
        {
            JellHp -= 1;
        }
    }
}
