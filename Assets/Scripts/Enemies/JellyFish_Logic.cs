using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyFish_Logic : MonoBehaviour
{

    public int JellHp = 8;
    public AudioSource DamageAudio;
    public Animator animator; 
    bool isDying = false;
    JellyFishShooting JS;

    // Start is called before the first frame update
    void Start()
    {
        JS = GetComponent<JellyFishShooting>();
    }

    // Update is called once per frame
    void Update()
    {
        if (JellHp <= 0)
        {
            JS.enabled = false;
            StartCoroutine(PlayDeathAndDestroy());
        }
    }


    IEnumerator PlayDeathAndDestroy()
    {
        isDying = true;
        animator.SetBool("Death", true);
        DamageAudio.Play();
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (isDying) return;

        if (collision.collider.CompareTag("PlayerGameObject"))
        {
            Player_hp.hp -= 1;
        }
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDying) return;

        if (collision.CompareTag("Bullet"))
        {
            JellHp -= 1;
            DamageAudio.Play();
        }

        if (collision.CompareTag("Crystal"))
        {
            JellHp -= 1;
            DamageAudio.Play();
        }
    }
}
