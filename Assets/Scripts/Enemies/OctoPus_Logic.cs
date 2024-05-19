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

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("PlayerGameObject");
    }

    void Update()
    {
        float distance = Vector2.Distance(player.transform.position, transform.position);

        if (distance <= viewrange && allowedToBlast)
        {
            StartCoroutine(StartBlasting());
        }

        if(OctoHp <= 0)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator StartBlasting()
    {
        allowedToBlast = false;
        float sprayTimer = 0f;

        while (sprayTimer < sprayDuration)
        {
            Instantiate(bulletprefab, transform.position, Quaternion.identity);
            sprayTimer += spawnInterval;
            yield return new WaitForSeconds(spawnInterval);
        }

        StartCoroutine(RunAway());
    }

    IEnumerator RunAway()
    {
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
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, viewrange);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       

        if (collision.collider.CompareTag("PlayerGameObject"))
        {
            Player_hp.hp -= 1;
            Debug.Log("Player take damage");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            OctoHp -= 1;
        }

        if (collision.CompareTag("Crystal"))
        {
            OctoHp -= 1;
        }
    }
}
