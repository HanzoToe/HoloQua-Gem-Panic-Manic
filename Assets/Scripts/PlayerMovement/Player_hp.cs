using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_hp : MonoBehaviour
{
    public static int hp = 3;
    public Animator animator; 
    public SpriteRenderer spriteRenderer;
    public Sprite deathsprite;
    public GameObject weaponhandler;
    public GameObject punchHandler;

    public Renderer rend;
    public Color c;



    // Start is called before the first frame update
    void Start()
    {
        
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
        }
    }
}
