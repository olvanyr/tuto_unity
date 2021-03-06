﻿using Unity.Mathematics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{


    //all the reference section in instance is called a singletone
    public static PlayerMovement instance; 
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("il y a plus d'une instance de PlayerMovement dans la scéne");
            return;
        }
        instance = this;
    }


    //used to store our movespeed
    public float moveSpeed;
    public float climbSpeed;

    public float jumpForce;

    //used to store the rigid body of the player
    public Rigidbody2D rb;    
    //used to store the capsule collider of the player
    public CapsuleCollider2D playerCol;

    
    private bool isJumping;

    [HideInInspector]
    public bool isGrounded;

    [HideInInspector]
    public bool isClimbing;

    //take reference for the empty gameobject that follow the player
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask collisionLayer;

    // we need that vectore to apply the velocity in the fonction
    private Vector3 velocity = Vector3.zero;

    //get the player animator
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    // variable to store the horizontal movment when we calculate it
    private float horizontalMovement;
    private float verticalMovement;

    void Update() 
    {
        
        //creat a float in witch we store the calculation of our movement
        horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.fixedDeltaTime;
        verticalMovement   = Input.GetAxis("Vertical")   * climbSpeed * Time.fixedDeltaTime;


        //horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime; //deltatime is used to make the movment the same whenever the game is a 30fps, 60fps,..

        if (Input.GetButtonDown("Jump") && isGrounded && !isClimbing)
        {
            isJumping = true;
        }


        flip(rb.velocity.x);

        // give to the animator the speed so wee can transition n the animation
        float charachterVelocity = math.abs((rb.velocity.x));
        animator.SetFloat("speed", charachterVelocity);
        animator.SetBool("isClimbing", isClimbing);
    }

    void FixedUpdate() // we use fixed update beacause we use the rigid body of the player and the internat physics to move the player fixedupdate is in step with the physique engine of unity so everything that use physique should be in a fixed update
    {

        //check if the space in between the two ground check object is coliding with something
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, collisionLayer);
        //execute the custome fonction MovePlayer
        MovePlayer(horizontalMovement, verticalMovement); 
    }

    //this custom fuction get one float argument 
    void MovePlayer(float _horizontalMovement, float _verticalMovement)
    {
        if (!isClimbing)
        {
            //creat a vector2 composed of our horizontal movement and the curent movement of the rigid body on y 
            Vector3 targetVelocity = new Vector2(_horizontalMovement, rb.velocity.y);

            // we execute a smooth (linear) damp beetween the actual velocity of the rb and our target velocity
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, 0.05f);

            if (isJumping)
            {
                rb.AddForce(new Vector2(0, jumpForce));
                isJumping = false;
            }
        }
        else
        {
            // vertical movment on the ladder
            Vector3 targetVelocity = new Vector2(0, _verticalMovement);
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, 0.05f);
        }
    }

    void flip(float _velocity)
    {
        if (_velocity > 0.1f)
        {
            spriteRenderer.flipX = false;
        }
        else if(_velocity < -0.1f)
        {
            spriteRenderer.flipX = true;
        }
    }
    
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
