using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{

    public static Vector2 Mouseposition;
    public GameObject Bulletprefab;
    public Transform BulletSpawn;
    public static  bool AllowedToShoot = true;
    private FaceMouse FM;
    private AimPunchTowardsMouse AM;
    public bool IsShooting = false; 

    [Header("Variables")]
    [SerializeField] private float shootingcooldown = 0.2f;
    [SerializeField] private int bulletinscene = 0;
    [SerializeField] private float FireRate = 0.5f; 
    

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        FM = GetComponentInChildren<FaceMouse>();
        AM = GetComponentInChildren<AimPunchTowardsMouse>();
    }

    // Update is called once per frame
    void Update()
    {

        //Get the mouse position on the screen and translate it to the game
        Mouseposition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Mouseposition.x = Mathf.Round(Mouseposition.x);
        Mouseposition.y = Mathf.Round(Mouseposition.y);

        FM.Pointerposition = Mouseposition;
        AM.Pointerposition = Mouseposition; 

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            AllowedToShoot = true;
            PunchScript.AllowedToPunch = false; 
        }


        if (Input.GetButton("Fire1") && AllowedToShoot && !PunchScript.AllowedToPunch&& bulletinscene < 4)
        {
            StartCoroutine(Shoot());
            bulletinscene++;
            IsShooting = true;
        }
    
        if(bulletinscene <= 4 && bulletinscene != 0)
        {
            FireRate -= Time.deltaTime;

            if (FireRate <= 0)
            {
                IsShooting = false;
                bulletinscene = 0;
                FireRate = 0.8f ;
            }
        }

    }


    IEnumerator Shoot()
    {
            AllowedToShoot = false;
            Instantiate(Bulletprefab, BulletSpawn.position, Quaternion.identity);
            yield return new WaitForSeconds(shootingcooldown);
            AllowedToShoot = true;
    }
}
