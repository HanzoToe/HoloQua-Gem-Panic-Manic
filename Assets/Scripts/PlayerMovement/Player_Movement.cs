using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] private float MovementSpeed = 5f;
    [SerializeField] private float SlideDistance = 10f;
    [SerializeField] private float SlideTime = 0.2f;
    [SerializeField] private float SlideCooldown = 0f; 
    
    [Header("Components")]
    [SerializeField] public Rigidbody2D rb;
    
    Vector2 direction;
    bool CanSlide = true;
    bool IsSliding = false;
    public Animator animator;
    public AudioSource DashAudio; 



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("WalkingAnimation", Mathf.Abs(direction.magnitude));

        //Gets the direction needed
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        //SlideCooldown goes from 2 to 0 using a set frame rate of seconds
        SlideCooldown -= Time.deltaTime; 

        //If Player Presses "Space" or whatever fucking button we use for controller
        if (Input.GetButtonDown("Fire3") && CanSlide && SlideCooldown <= 0f)
        {
            DashAudio.Play();
            StartCoroutine("Slide");
            SlideCooldown = 1f;
        }

    }


    IEnumerator Slide()
    {

        

        // Store Original Position
        Vector2 initialPosition = transform.position;

        //Calculating the target position on slide direction and distance
        Vector2 targetposition = initialPosition + (direction.normalized * SlideDistance);

        //Say no no to slide
        CanSlide = false;

        //Sliding yes
        IsSliding = true; 

        //Slide towards the target position over SlideTime
        float elapsedtime = 0f;
        while(elapsedtime < SlideTime)
        {
            animator.SetBool("Dashing", true);

            //Calculate interpolation factor (0 and 1) based on elapsed time and slide distance 
            float t = elapsedtime / SlideTime;

            //Move the player towards the target position
            //Lerp is a math function that returns a value between 2 others at a point on a linear scale
            rb.MovePosition(Vector2.Lerp(initialPosition, targetposition, t));

            //Increment elapsed time
            elapsedtime += Time.deltaTime;

            //Wait for the next frame
            yield return null;
        }

        //Make sure the player ends up a targetposition
        rb.MovePosition(targetposition);

        //Say yes yes to slide
        CanSlide = true;

        //Sliding no
        IsSliding = false;

        animator.SetBool("Dashing", false);
    }



    private void FixedUpdate()
    {
        rb.velocity = direction * MovementSpeed; 
    }
}
