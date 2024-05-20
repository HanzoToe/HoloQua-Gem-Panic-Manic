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

    public AudioSource DamageAudio; 
    public GameObject player;
    public Vector2 Moving;
    public int ShrimpHp = 3;
    public Animator animator;
    bool isDying = false; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (isDying) return;

        player = GameObject.FindGameObjectWithTag("PlayerGameObject");
        rb = GetComponent<Rigidbody2D>();

        float distance = Vector2.Distance(transform.position, player.transform.position);

        if(distance <= ViewDistance)
        {
            MoveTowardsPlayer();
            animator.SetBool("Walking", true); 

        }
        else if(distance > ViewDistance)
        {
            rb.velocity = Vector2.zero;
            animator.SetBool("Walking", false);
        }

       

        if(ShrimpHp <= 0)
        {

            StartCoroutine(PlayDeathAndDestroy());
        }
    }

    IEnumerator PlayDeathAndDestroy()
    {
        isDying = true;
        animator.SetBool("Death", true);
        DamageAudio.Play();
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);

    }


    private void MoveTowardsPlayer()
    {
        if (isDying) return;

        Moving = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y);
        rb.velocity = Moving.normalized * MovementSpeed;

        Vector2 scale = transform.localScale;
        if (Moving.x < 0)
        {
            scale.x = -1f;
        }
        else if (Moving.x > 0)
        {
            scale.x = 1f;
        }
        transform.localScale = scale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDying) return;

        if (collision.collider.CompareTag("PlayerGameObject"))
        {
            Player_hp.hp -= 1;
            StartCoroutine(PlayDeathAndDestroy());
        }

        if (collision.collider.CompareTag("Pillar"))
        {
            PillarHP pillarHP = collision.collider.GetComponent<PillarHP>();

            if (pillarHP != null)
            {
                pillarHP.Pillarhp = 0;
                pillarHP.SetDestroyedByShrimp();
            }

            StartCoroutine(PlayDeathAndDestroy());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDying) return;

        if (collision.CompareTag("Bullet"))
        {
            ShrimpHp -= 1;
            DamageAudio.Play();
        }

        if (collision.CompareTag("Crystal"))
        {
            ShrimpHp -= 1;
            DamageAudio.Play();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, ViewDistance);
    }
}
