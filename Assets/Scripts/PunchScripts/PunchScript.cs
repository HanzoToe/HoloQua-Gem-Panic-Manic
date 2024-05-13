    using System;
    using System.Collections;
    using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

    public class PunchScript : MonoBehaviour
    {
        public static bool AllowedToPunch = false;
        public GameObject PunchPoint;
        public float radius = 4f;
        public LayerMask enemyLayer;
        public LayerMask PillarLayer;
        public float Cooldown = 0f;
        public SpriteRenderer SR;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
           Cooldown -= Time.deltaTime; 

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                AllowedToPunch = true;
                Shooting.AllowedToShoot = false; 
            }

           if (Input.GetButton("Fire1") && AllowedToPunch && !Shooting.AllowedToShoot && Cooldown <= 0f)
            {
                Attack();
                Cooldown = 0.8f;
            }
        }

    private void Attack()
        {
            Collider2D[] enemy = Physics2D.OverlapCircleAll(PunchPoint.transform.position, radius, enemyLayer);
            Collider2D[] pillar = Physics2D.OverlapCircleAll(PunchPoint.transform.position, radius, PillarLayer);

            foreach (Collider2D enemyGameObject in enemy)
            {
                Debug.Log("Enemy hit"); 
            }

            foreach (Collider2D PillarGameObject in pillar)
            {
                Debug.Log("Pillar hit");
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(PunchPoint.transform.position, radius);
        }
    }
