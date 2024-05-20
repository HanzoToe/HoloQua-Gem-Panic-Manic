using System.Collections;
using UnityEngine;

public class OctoPus_Logic : MonoBehaviour
{
    private Rigidbody2D rb;
    public GameObject player;
    public GameObject bulletprefab;
    public float movespeed = 3f;
    public float viewrange = 5f;
    public float sprayDuration = 3f;  // Duration of spraying bullets
    public float spawnInterval = 0.2f; // Interval between bullet spawns
    private bool allowedToBlast = true;
    private Vector2 moving;
    public int OctoHp = 4;
    public AudioSource DamageAudio;
    public Animator animator;
    bool isDying = false;
    Vector2 PlayerPos; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("PlayerGameObject");
    }

    void Update()
    {
        if (isDying) return;

        float distance = Vector2.Distance(player.transform.position, transform.position);

        if (distance <= viewrange && allowedToBlast)
        {
            StartCoroutine(StartBlasting());
        }

        if(OctoHp <= 0)
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


    IEnumerator StartBlasting()
    {


        allowedToBlast = false;
        float sprayTimer = 0f;

        animator.SetBool("Shooting", true);
        animator.SetBool("Tired", false);

        while (sprayTimer < sprayDuration)
        {
            Instantiate(bulletprefab, transform.position, Quaternion.identity);
            sprayTimer += spawnInterval;
            PlayerPos = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y);

            Vector2 scale = transform.localScale;
            if (PlayerPos.x < 0)
            {
                scale.x = -1f;
            }
            else if (PlayerPos.x > 0)
            {
                scale.x = 1f;
            }
            transform.localScale = scale;
            yield return new WaitForSeconds(spawnInterval);
        }

        StartCoroutine(RunAway());
    }

    IEnumerator RunAway()
    {
        animator.SetBool("Shooting", false);
        animator.SetBool("Tired", true);

        float runAwayDuration = 3f; // Adjust this value as needed

        while (runAwayDuration > 0f)
        {
            moving = new Vector2(transform.position.x - player.transform.position.x, transform.position.y - player.transform.position.y);
            rb.velocity = moving.normalized * movespeed;
            runAwayDuration -= Time.deltaTime;
            yield return null;
        }

        rb.velocity = Vector3.zero;
        allowedToBlast = true;
        animator.SetBool("Shooting", false);
        animator.SetBool("Tired", false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, viewrange);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDying) return;

        if (collision.collider.CompareTag("PlayerGameObject"))
        {
            Player_hp.hp -= 1;
            Debug.Log("Player take damage");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDying) return;

        if (collision.CompareTag("Bullet"))
        {
            OctoHp -= 1;
            DamageAudio.Play();
        }

        if (collision.CompareTag("Crystal"))
        {
            OctoHp -= 1;
            DamageAudio.Play();
        }
    }
}
