using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]

public class PlayerController : MonoBehaviour    
{

    bool coroutineRunning = false;
    bool shootingCoroutineRunning = false;
    bool hurtCoroutineRunning = false;

    bool stunCharacter = false;
    bool isGrounded = false;
    bool isShooting = false;

    public bool isHurt = false;
    bool isClimbing = false;
    public int health = 28;
    public int ammo = 56;

    Animator anim;

    Rigidbody2D rb;
    SpriteRenderer pr;

    int _score = 0;
    int _lives = 3;
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
    [SerializeField] public int jumpForce;

    [SerializeField] LayerMask isDeathBoxLayer;
    [SerializeField] LayerMask isGroundLayer;

    [SerializeField] LayerMask isLadderLayer;
    [SerializeField] LayerMask isMantleLayer;

    [SerializeField] Transform groundCheck;

    [SerializeField] Collider2D groundCheckCollider;
    [SerializeField] Collider2D ladderCheckCollider;

    [SerializeField] Image healthBar;
    [SerializeField] Image ammoBar;
    [SerializeField] Text scoreText;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        
        if (speed <= 0) { speed = 5.0f; }
        if (jumpForce <= 0) { jumpForce = 375; }
        if (stunCharacter) { stunCharacter = false; }
        if (!groundCheck)
        {
            groundCheck.transform.GetChild(0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 1)
        {
            isShooting = false;
            handleUI();
            float hInput = Input.GetAxis("Horizontal");
            Vector2 moveDir = new Vector2(hInput * speed, rb.velocity.y);

            if (health <= 0)
            {
                anim.SetBool("Death", true);
                lives--;
                health = 28;
            }
            else
            {
                anim.SetBool("Death", false);
            }

            if (!stunCharacter || !isHurt)
            {
                isGrounded = groundCheckCollider.IsTouchingLayers(isGroundLayer);

                if (groundCheckCollider.IsTouchingLayers(isDeathBoxLayer)) health = 0;

                if (isGrounded && Input.GetButtonDown("Jump"))
                {
                    rb.velocity = Vector2.zero;
                    rb.AddForce(Vector2.up * jumpForce);
                }


                if (!(rb.IsTouchingLayers(isLadderLayer))) anim.enabled = true;

                anim.SetBool("Climbing", ladderCheckCollider.IsTouchingLayers(isLadderLayer));
                anim.SetBool("Mantle", ladderCheckCollider.IsTouchingLayers(isMantleLayer));
                if (ladderCheckCollider.IsTouchingLayers(isLadderLayer) && Input.GetKey(KeyCode.W))
                {
                    anim.enabled = true;
                    moveDir.y = 3;
                }
                if (ladderCheckCollider.IsTouchingLayers(isLadderLayer) && !(Input.GetKey(KeyCode.W)))
                {
                    //anim.Play("Climbing");
                    moveDir.y = 0.201f;
                    anim.enabled = false;
                }

                rb.velocity = moveDir;

                if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
                {
                    stunCharacter = true;
                    anim.SetBool("stunCharacter", stunCharacter);
                }

                if (hInput > 0 && !pr.flipX || hInput < 0 && pr.flipX)
                {
                    pr.flipX = !pr.flipX;
                    anim.SetBool("flipX", pr.flipX);
                }

                if (Mathf.Abs(hInput) <= 0.1f) moveDir.x = 0;

                anim.SetFloat("xVel", Mathf.Abs(hInput));
                anim.SetFloat("yVel", Mathf.Abs(rb.velocity.y));
                anim.SetBool("isGrounded", isGrounded);
            }

            if (isHurt)
            {
                StartHurtDelay();
            }
        }
    }

    private void handleUI()
    {

        float ammoTemp = 1f / 56f;
        if (Input.GetButtonDown("Fire1"))
        {
            if (ammo > 56)
            {
                ammo = 56;
            }
            if (ammo != 0 && ammo > 0)
            {
                isShooting = true;
                anim.SetBool("isShooting", isShooting);
                ammo--;
                StartShootingDelay();
            }
            else ammo = 0;
        }

        float healthTemp = 1f / 28f;
        if (health != 0 && health < 0)
        {
            health = 28;
            lives--;
        }

        healthBar.fillAmount = health * healthTemp;
        ammoBar.fillAmount = ammo * ammoTemp;
        scoreText.text = score.ToString();

    }

    public void StartHurtDelay()
    {
        if (!hurtCoroutineRunning)
        {
            StartCoroutine("HurtDelay");
        }
        else
        {
            StopCoroutine("HurtDelay");
        }
    }

    IEnumerator HurtDelay()
    {
        hurtCoroutineRunning = true;

        anim.SetBool("Hurt", false);
        yield return new WaitForSeconds(1.5f);
        anim.SetBool("Hurt", false);

        hurtCoroutineRunning = false;
    }

    public void unStunPlayer()
    {
        stunCharacter = false;
        anim.SetBool("stunCharacter", stunCharacter);
        anim.SetBool("isShooting", false);
    }

    public void StartShootingDelay()
    {
        if (!shootingCoroutineRunning)
        {
            StartCoroutine("ShootingDelay");
        }
        else
        {
            StopCoroutine("ShootingDelay");
        }
    }

    IEnumerator ShootingDelay()
    {
        shootingCoroutineRunning = true;

        isShooting = true;
        anim.SetBool("isShooting", isShooting);
        yield return new WaitForSeconds(2.0f);
        isShooting = false;
        anim.SetBool("isShooting", isShooting);

        shootingCoroutineRunning = false;
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


