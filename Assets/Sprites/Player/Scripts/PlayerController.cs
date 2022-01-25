using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]

public class PlayerController : MonoBehaviour    
{

    public bool verboose = true;
    public bool isGrounded = false;
    Animator anim;

    Rigidbody2D rb;
    SpriteRenderer pr;

    [SerializeField] public float speed;

    [SerializeField] public int jumpForce;
    [SerializeField] public float groundCheckRadius;

    [SerializeField] LayerMask isGroundLayer;
    [SerializeField] Transform groundCheck;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        
        if (speed <= 0) { speed = 5.0f; }
        if (jumpForce <= 0) { jumpForce = 300; }
        if (groundCheckRadius <= 0) { groundCheckRadius = 0.05f; }
        if (!groundCheck) { groundCheck.transform.GetChild(0); }

    }

    // Update is called once per frame
    void Update()
    {
        float hInput = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxis("Vertical");

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * jumpForce);
        }

        Vector2 moveDir = new Vector2(hInput * speed, rb.velocity.y);
        rb.velocity = moveDir;

        if (hInput > 0) { pr.flipX = true; }
        if (hInput < 0) { pr.flipX = false; }

        anim.SetFloat("xVel", Mathf.Abs(hInput));
        anim.SetBool("isGrounded", isGrounded);
    }
}

