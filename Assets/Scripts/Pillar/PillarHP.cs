using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarHP : MonoBehaviour
{
    public int Pillarhp = 40;
    public GameObject ShotgunBullets;
    public bool destroyedbypunch = false;
    public bool destroyedbyshrimp = false; 
    public Transform bulletspawnPoint;
    public Transform ShrimpBulletSpawnPoint; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Pillarhp <= 0)
        {
            if (destroyedbypunch)
            {   
                Spawnbullet();
            }

            if (destroyedbyshrimp)
            {
                SpawnBulletShrimp();
            }

            Destroy(gameObject);
        }
    }

    private void SpawnBulletShrimp()
    {
        Instantiate(ShotgunBullets, ShrimpBulletSpawnPoint.transform.position, ShrimpBulletSpawnPoint.rotation);
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
