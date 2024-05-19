using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyFishShooting : MonoBehaviour
{
    public Transform shootingspawn;
    public GameObject player; 
    public GameObject bulletprefab;
    public float viewrange = 5f;
    public int bulletsshot = 0;
    public float timer = 0f;
    public float shootingcooldown = 0.2f; 
    


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("PlayerGameObject"); 
    }

    // Update is called once per frame
    void Update()
    {
        float Distance = Vector2.Distance(player.transform.position, transform.position);

        if (Distance <= viewrange && bulletsshot < 1)
        {
            StartCoroutine(Shoot());
            bulletsshot++;
            
        }

        
        if(bulletsshot >= 1)
        {
            timer += Time.deltaTime; 
        }

        if (timer >= 0.8f)
        {
            bulletsshot = 0;
            timer = 0f; 
        }

    }

    IEnumerator Shoot()
    {
            Instantiate(bulletprefab, shootingspawn.position, shootingspawn.rotation);
            yield return null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, viewrange);
    }
}
