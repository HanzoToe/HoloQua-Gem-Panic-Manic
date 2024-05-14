    using System;
    using System.Collections;
    using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

    public class PunchScript : MonoBehaviour
    {
        public GameObject PunchPoint;
        public LayerMask enemyLayer;
        public LayerMask PillarLayer;

        [Header("Variables")]
        [SerializeField] private float Cooldown = 0f;
        [SerializeField] private float radius = 4f;
        [SerializeField] private int Damage = 3;
        [SerializeField] private int DamageToCrystal = 10; 
     

          // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            Cooldown -= Time.deltaTime; 

            if (Input.GetKeyDown(KeyCode.E) && Cooldown <= 0f)
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
                PillarHP pillarhp = PillarGameObject.GetComponent<PillarHP>();
                if (pillarhp != null)
                {
                 pillarhp.Pillarhp -= DamageToCrystal;
                 if(pillarhp.Pillarhp <= 1)
                 {
                    pillarhp.SetDestroyedByPunch();
                 }
                 
                }
                
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(PunchPoint.transform.position, radius);
      }


       
    }
