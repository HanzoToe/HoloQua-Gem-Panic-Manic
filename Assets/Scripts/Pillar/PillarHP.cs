using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarHP : MonoBehaviour
{
    public int Pillarhp = 40;
    public GameObject ShotgunBullets;
    public bool destroyedbypunch = false;
    public Transform bulletspawnPoint; 

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
            Destroy(gameObject);
        }
    }



    private void Spawnbullet()
    {

        Instantiate(ShotgunBullets, bulletspawnPoint.transform.position, bulletspawnPoint.rotation);
    }

    public void SetDestroyedByPunch()
    {
        destroyedbypunch = true; 
    }
}
