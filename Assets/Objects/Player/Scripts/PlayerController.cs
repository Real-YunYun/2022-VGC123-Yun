using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]

public class PlayerController : MonoBehaviour    
{

    bool coroutineRunning = false;
    bool shootingCoroutineRunning = false;

    public bool stunCharacter = false;
    bool isGrounded = false;

    public int health = 28;
    public int ammo = 28;

    Animator anim;
    Rigidbody2D rb;
    SpriteRenderer pr;
    PlayerSounds sounds;

    [SerializeField] public float speed;
    [SerializeField] public float jumpForce;

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

    [SerializeField] AudioMixerGroup group;
    [SerializeField] AudioClip PlayerDeathSound;
    [SerializeField] AudioClip PlayerHitSound;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        sounds = GetComponent<PlayerSounds>();
        
        if (speed <= 0) { speed = 4.0f; }
        if (jumpForce <= 0) { jumpForce = 300; }
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
            handleUI();
            float hInput = Input.GetAxis("Horizontal");
            Vector2 moveDir = new Vector2(hInput * speed, rb.velocity.y);

            if (groundCheckCollider.IsTouchingLayers(isDeathBoxLayer))
            {
                health = 0;
                sounds.Play(PlayerDeathSound);
                anim.Play("Death");
            }
            if (health <= 0) anim.Play("Death");
            else anim.SetBool("Death", false);

            if (!stunCharacter)
            {
                isGrounded = groundCheckCollider.IsTouchingLayers(isGroundLayer);

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
                    rb.gravityScale = 1;
                }
                if (ladderCheckCollider.IsTouchingLayers(isLadderLayer) && !(Input.GetKey(KeyCode.W)))
                {
                    //anim.Play("Climbing");
                    moveDir = Vector2.zero;
                    rb.gravityScale = 0;
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
        }
    }

    private void handleUI()
    {
        float ammoTemp = 1f / 56f;
        if (Input.GetButtonDown("Mouse ScrollWheel"))
        {
            if (ammo > 56)
            {
                ammo = 56;
            }
            if (ammo != 0 && ammo > 0)
            {
                ammo--;
                StartShootingDelay();
            }
            else ammo = 0;
        }

        float healthTemp = 1f / 28f;
        if (health != 0 && health < 0)
        {
            GameManager.state.lives--;
        }
        else if (health > 28) health = 28;

        healthBar.fillAmount = health * healthTemp;
        ammoBar.fillAmount = ammo * ammoTemp;
        scoreText.text = GameManager.state.score.ToString();
    }

    public void StartHurtDelay() 
    {
        sounds.Play(PlayerHitSound);
        anim.Play("Hurt");
    }

    public void PlayPickUpSound(AudioClip sound)
    {
        sounds.Play(sound);
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
            shootingCoroutineRunning = true;
            StartCoroutine("ShootingDelay");
        }
        else if (shootingCoroutineRunning)
        {
            StopCoroutine("ShootingDelay");
            anim.SetBool("isShooting", false);
            StartCoroutine("ShootingDelay");
        }
        else
        {
            StopCoroutine("ShootingDelay");
        }
    }

    IEnumerator ShootingDelay()
    {
        anim.SetBool("isShooting", true);

        yield return new WaitForSeconds(1);

        shootingCoroutineRunning = false;
        anim.SetBool("isShooting", false);
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
        jumpForce *= 1.25f;

        yield return new WaitForSeconds(5.0f);

        jumpForce /= 1.25f;
        coroutineRunning = false;
    }

    public void Death()
    {
        GameManager.state.lives--;
        SceneManager.LoadScene("Game Over");
    }
}


