using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]

public class PlayerController : MonoBehaviour    
{

    bool coroutineRunning = false;

    public bool verboose = true;
    public bool isGrounded = false;
    public bool stunCharacter = false;
    public bool isShooting = false;
    public int delayShooting = 60;
    public int health = 28;
    Animator anim;

    Rigidbody2D rb;
    SpriteRenderer pr;

    int _score = 0;
    int _lives = 1;
    public int maxLives = 3;

    public int score
    {
        get { return _score; }
        set
        {
            _score = value;
        }
    }

    public int lives
    {
        get { return _lives; }
        set
        {
            //Respawn
            //if (_lives > value)

            _lives = value;
            if (_lives > maxLives)
            {
                _lives = maxLives;
            }

            // Gameover screen?
            //if (_lives < 0)
        }
    }

    [SerializeField] public float speed;

    [SerializeField] public float groundCheckRadius;
    [SerializeField] public int jumpForce;

    [SerializeField] LayerMask isDeathBoxLayer;
    [SerializeField] LayerMask isGroundLayer;

    [SerializeField] Transform groundCheck;

    [SerializeField] Collider2D groundCheckCollider;

    [SerializeField] Image healthBar;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        
        if (speed <= 0) { speed = 5.0f; }
        if (jumpForce <= 0) { jumpForce = 300; }
        if (stunCharacter) { stunCharacter = false; }
        if (groundCheckRadius <= 0) { groundCheckRadius = 0.05f; }
        if (!groundCheck)
        {
            groundCheck.transform.GetChild(0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        isShooting = false;
        handleHealthUI();
        float hInput = Input.GetAxis("Horizontal");
        Vector2 moveDir = new Vector2(hInput * speed, rb.velocity.y);
        
        if (!stunCharacter)
        {
            isGrounded = groundCheckCollider.IsTouchingLayers(isGroundLayer);

            Debug.Log(rb.IsTouchingLayers(isDeathBoxLayer));
            if (rb.IsTouchingLayers(isDeathBoxLayer))
            {
                health = 0;
            }

            if (isGrounded && Input.GetButtonDown("Jump"))
            {
                rb.velocity = Vector2.zero;
                rb.AddForce(Vector2.up * jumpForce);
            }
            
            rb.velocity = moveDir;

            if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)) 
            { 
                stunCharacter = true; 
                anim.SetBool("stunCharacter", stunCharacter);
            }

            if (Input.GetButtonDown("Fire1"))
            {
                isShooting = true;
                anim.SetBool("isShooting", isShooting);
                delayShooting = 1000;
                delayShooting -= 1;
                //anim.SetBool("isShooting", Input.GetButtonDown("Fire1"));
            }

            if (delayShooting != 1000)
            {
                delayShooting -= 1;
            }

            if (hInput > 0 && !pr.flipX || hInput < 0 && pr.flipX)
            {
                pr.flipX = !pr.flipX;
                anim.SetBool("flipX", pr.flipX);
            }

            if (Mathf.Abs(hInput) <= 0.1f ) moveDir.x = 0;

            anim.SetFloat("xVel", Mathf.Abs(hInput));
            anim.SetFloat("yVel", Mathf.Abs(rb.velocity.y));
            anim.SetBool("isGrounded", isGrounded);
            if (delayShooting <= 0)
            {
                isShooting = false;
                anim.SetBool("isShooting", isShooting);
                delayShooting = 1000;
            }
        } 
        if (stunCharacter)  
        {
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !anim.IsInTransition(0))
            {
                stunCharacter = false;
                anim.SetBool("isShooting", false);
                anim.SetBool("stunCharacter", stunCharacter);
            }
        }
    }

    private void handleHealthUI()
    {

        //HealthBar has 7 full segments of units of 4, 7 * 4 = 28 health in total
        float temp = 1f / 28f;
        healthBar.fillAmount = health * temp;
        if (Input.GetButtonDown("Fire2"))
        {
            if (health != 0 && health > 0)
            {
                health--;
            }
            else
            {
                health = 0;
            }
        }
    }

    public void StartJumpForceChange()
    {
        if (!coroutineRunning)
        {
            StartCoroutine("JumpForceChange");
        }
        else
        {
            StopCoroutine("JumpForceChange");
            jumpForce /= 2;
            StartCoroutine("JumpForceChange");
        }
    }

    IEnumerator JumpForceChange()
    {
        coroutineRunning = true;
        jumpForce *= 2;

        yield return new WaitForSeconds(5.0f);

        jumpForce /= 2;
        coroutineRunning = false;
    }
}


