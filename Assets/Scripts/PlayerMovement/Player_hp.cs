using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class Player_hp : MonoBehaviour
{
    public int maxhp = 4; 
    public static int hp;
    public float IFramesDuration = 3f;
    public int numberofflashes = 3; 
    public Animator animator; 
    public SpriteRenderer spriteRenderer;
    public Sprite deathsprite;
    public GameObject weaponhandler;
    public GameObject punchHandler;

    public SpriteRenderer rend;
    public Player_Movement pm;
    public Shooting SS;
    public Pillar pil;
    public PunchScript PS;
    public Collider2D collider;


    public static event Action<int> OnHpChanged;


    // Start is called before the first frame update
    void Start()
    {
        hp = maxhp;
        OnHpChanged?.Invoke(hp);
    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log(hp);


        if(hp <= 0)
        {
            animator.SetBool("Death", true);
            spriteRenderer.sprite = deathsprite;
            weaponhandler.SetActive(false);
            punchHandler.SetActive(false);
            pm.rb.velocity = Vector3.zero;
            pm.enabled = false;
            SS.enabled = false;
            pil.enabled = false;
            PS.enabled = false;
            collider.enabled = false;
        }

        

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Shrimp") || collision.collider.CompareTag("Jelly") || collision.collider.CompareTag("Shork") || collision.collider.CompareTag("Octo"))
        {
            OnHpChanged?.Invoke(hp);

            if (hp > 0)
            {
                StartCoroutine(Invulnerability());
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Crystal") || collision.CompareTag("EnemyBoolet"))
        {
            OnHpChanged?.Invoke(hp);

            if(hp > 0 )
            {
                StartCoroutine(Invulnerability());
            }
            
        }
    }




    private IEnumerator Invulnerability()
    {
        Physics2D.IgnoreLayerCollision(7, 8, true);
        Physics2D.IgnoreLayerCollision(7, 9, true);
        Physics2D.IgnoreLayerCollision(7, 10, true);
        Physics2D.IgnoreLayerCollision(7, 11, true);
        for (int i = 0; i < numberofflashes; i++)
        {
            rend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(IFramesDuration / (numberofflashes * 2));
            rend.color = Color.white;
            yield return new WaitForSeconds(IFramesDuration / (numberofflashes * 2));
        }
        Physics2D.IgnoreLayerCollision(7, 8, false);
        Physics2D.IgnoreLayerCollision(7, 9, false);
        Physics2D.IgnoreLayerCollision(7, 10, false);
        Physics2D.IgnoreLayerCollision(7, 11, false);
    }
}
