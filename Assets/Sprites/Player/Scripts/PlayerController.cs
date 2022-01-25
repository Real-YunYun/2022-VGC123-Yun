using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]

public class PlayerController : MonoBehaviour    
{

    public bool verboose = true;
    public bool isGrounded = false;
    public bool stunCharacter = false;
    public bool isShooting = false; //temp Variable
    public int delayShooting = 60;
    Animator anim;

    Rigidbody2D rb;
    SpriteRenderer pr;

    [SerializeField] public float speed;

    [SerializeField] public float groundCheckRadius;
    [SerializeField] public int jumpForce;

    [SerializeField] LayerMask isGroundLayer;
    [SerializeField] Transform groundCheck;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        
        if (speed <= 0) { speed = 5.0f; }
        if (jumpForce <= 0) { jumpForce = 300; }
        if (stunCharacter) { stunCharacter = false; }
        if (groundCheckRadius <= 0) { groundCheckRadius = 0.05f; }
        if (!groundCheck) { groundCheck.transform.GetChild(0); }


    }

    // Update is called once per frame
    void Update()
    {
        float hInput = Input.GetAxis("Horizontal");
        Vector2 moveDir = new Vector2(hInput * speed, rb.velocity.y);
        
        if (!stunCharacter)
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);
                
            if (isGrounded && Input.GetButtonDown("Jump"))
            {
                rb.velocity = Vector2.zero;
                rb.AddForce(Vector2.up * jumpForce);
            }
            
            rb.velocity = moveDir;

            if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)) { stunCharacter = true; anim.SetBool("stunCharacter", stunCharacter); }

            if (hInput > 0 && !pr.flipX || hInput < 0 && pr.flipX) pr.flipX = !pr.flipX;

            if (Mathf.Abs(hInput) <= 0.1f ) moveDir.x = 0;

            anim.SetFloat("xVel", Mathf.Abs(hInput));
            anim.SetFloat("yVel", Mathf.Abs(rb.velocity.y));
            anim.SetBool("Shooting", Input.GetKey(KeyCode.E));
            anim.SetBool("isGrounded", isGrounded);
        } 
        if (stunCharacter)  
        {
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !anim.IsInTransition(0))
            {
                stunCharacter = false;
                anim.SetBool("Shooting", false);
                anim.SetBool("stunCharacter", stunCharacter);
            }
        }
    }
}

