using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarHP : MonoBehaviour
{
    public int Pillarhp = 40;
    public GameObject ShotgunBullets;
    public GameObject EnemyShotgunBullets; 
    public bool destroyedbypunch = false;
    public bool destroyedbyshrimp = false; 
    public Transform bulletspawnPoint;
    public Transform ShrimpBulletSpawnPoint;
    public LayerMask enemybullets; 
    public Sprite CrackedHpCrsytals;
    public Sprite destroyedcrystal;
    public SpriteRenderer sr; 
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
          if(Pillarhp <= 10)
          {
            animator.enabled = false;
            sr.sprite = CrackedHpCrsytals;
          }

        if(Pillarhp <= 0)
        {
            if (destroyedbypunch)
            {
                sr.sprite = destroyedcrystal;
                Spawnbullet();
            }

            if (destroyedbyshrimp)
            {
                sr.sprite = destroyedcrystal;
                SpawnBulletShrimp();
            }

            Destroy(gameObject);
        }
    }

    private void SpawnBulletShrimp()
    {
        Instantiate(EnemyShotgunBullets, ShrimpBulletSpawnPoint.transform.position, ShrimpBulletSpawnPoint.rotation);
    }

    private void Spawnbullet()
    {

        Instantiate(ShotgunBullets, bulletspawnPoint.transform.position, bulletspawnPoint.rotation);
    }



    public void SetDestroyedByPunch()
    {
        destroyedbypunch = true; 
    }

    public void SetDestroyedByShrimp()
    {
        destroyedbyshrimp = true;
    }
}
