using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillar : MonoBehaviour
{
    Vector2 MousePos;
    Collider2D hitcollider;
    GameObject SpawnedPillar; 

    [Header("Variables")]
    [SerializeField] private float PlaceRange = 3f;
    [SerializeField] private GameObject pillar;
    [SerializeField] private LayerMask PillarLayer;
    [SerializeField] private float CooldownTimer = 0f; 


    // Start is called before the first frame update
    void Start()
    {
        
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
            hitcollider = Physics2D.OverlapCircle(MousePos, 0.4f , PillarLayer);
            
            if (hitcollider == null) 
            {
                if(SpawnedPillar != null)
                {
                    Destroy(SpawnedPillar);
                }

               SpawnedPillar = Instantiate(pillar, MousePos, Quaternion.identity);
                CooldownTimer = 2f; 
            }
          
            Debug.Log(hitcollider);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, PlaceRange);
    }
}
