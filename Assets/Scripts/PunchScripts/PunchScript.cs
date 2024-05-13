    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class PunchScript : MonoBehaviour
    {
        public static bool AllowedToPunch = false;
        public GameObject PunchPoint;
        public float radius = 4f;
        public LayerMask enemyLayer;
        public LayerMask PillarLayer;
        public float MovementSpeed = 5f; 

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
        


            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                AllowedToPunch = true;
                Shooting.AllowedToShoot = false; 
            }

            if(Input.GetButtonDown("Fire1") && AllowedToPunch)
            {
                Attack();
              MoveTowardsMousePosition();
            }
        }

    private void MoveTowardsMousePosition()
    {
        Vector3 PunchMoveTowards = PunchPoint.transform.position;
        PunchMoveTowards.z = 0f;

        Vector3 direction = PunchMoveTowards - transform.position;
        direction.z = 0f;
        direction.Normalize();

        transform.position += direction * MovementSpeed * Time.deltaTime;
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
