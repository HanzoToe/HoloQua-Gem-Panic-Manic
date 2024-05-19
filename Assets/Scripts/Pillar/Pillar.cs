using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillar : MonoBehaviour
{
    Vector2 MousePos;
    Collider2D hitcollider;
    Collider2D playerCollider;
    Collider2D enemyCollider; 
    GameObject SpawnedPillar;
    LineRenderer lineRenderer;

    [Header("Variables")]
    [SerializeField] private float PlaceRange = 3f;
    [SerializeField] private GameObject pillar;
    [SerializeField] private LayerMask PillarLayer;
    [SerializeField] private LayerMask PlayerLayer;
    [SerializeField] private LayerMask EnemyLayer;
    [SerializeField] private float CooldownTimer = 0f;
    [SerializeField] private int circleSegments = 100;



    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = circleSegments + 1;
        lineRenderer.loop = true;
        lineRenderer.useWorldSpace = false;
        DrawCircle();
    }

    private void DrawCircle()
    {
        float angle = 0f;
        for (int i = 0; i < circleSegments + 1; i++)
        {
            float x = Mathf.Sin(Mathf.Deg2Rad * angle) * PlaceRange;
            float y = Mathf.Cos(Mathf.Deg2Rad * angle) * PlaceRange;

            lineRenderer.SetPosition(i, new Vector3(x, y, 0));
            angle += 360f / circleSegments;
        }
    }

    // Update is called once per frame
    void Update()
    {

        CooldownTimer -= Time.deltaTime;


        MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        MousePos.x = Mathf.Round(MousePos.x);
        MousePos.y = Mathf.Round(MousePos.y);

        float distance = Vector2.Distance(transform.position, MousePos);

      

        if (Input.GetButtonDown("Fire2") && distance <= PlaceRange && CooldownTimer <= 0f)
        {
            hitcollider = Physics2D.OverlapCircle(MousePos, 0.4f, PillarLayer);
            playerCollider = Physics2D.OverlapCircle(MousePos, 0.4f, PlayerLayer);
            enemyCollider = Physics2D.OverlapCircle(MousePos, 0.4f, EnemyLayer);


            if (hitcollider == null && playerCollider == null && enemyCollider == null) 
            {
                if(SpawnedPillar != null)
                {
                    Destroy(SpawnedPillar);
                }

               SpawnedPillar = Instantiate(pillar, MousePos, Quaternion.identity);
                CooldownTimer = 5f; 
            }
          
            Debug.Log(hitcollider);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, PlaceRange);
    }
}
