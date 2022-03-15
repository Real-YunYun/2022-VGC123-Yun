using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Collider2D))]

public class PepeController : MonoBehaviour
{
    public int health = 5;

    Rigidbody2D rb;
    Collider2D c2;
    SpriteRenderer sr;
    PlayerSounds ps;

    [SerializeField] SpawnPickUps spawnerPickUp;
    [SerializeField] LayerMask isStopWallLayer;
    [SerializeField] LayerMask isPlayerLayer;
    [SerializeField] AudioClip OnHitSound;
    [SerializeField] AudioClip OnDeathSound;

    public Vector2 speed;
    public bool flipped = false;
    public bool inLayerMask = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        c2 = GetComponent<Collider2D>();
        sr = GetComponent<SpriteRenderer>();
        ps = GetComponent<PlayerSounds>();
        speed.x = 2f;
        speed.y = 0f;
    }

    // Update is called once per frame
    void Update()
    {

        if (flipped) rb.velocity = speed;
        else rb.velocity = -speed;

        if (c2.IsTouchingLayers(isStopWallLayer) && !inLayerMask)
        {
            inLayerMask = true;
            flipped = !flipped;
            sr.flipX = flipped;
        }
        if (!c2.IsTouchingLayers(isStopWallLayer) && inLayerMask)
        {
            inLayerMask = false;
        }
        if (health <= 0)
        {
            ps.Play(OnDeathSound);
            spawnerPickUp.spawnPickUpOnUpdate();
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController curPlayerController = collision.gameObject.GetComponent<PlayerController>();
        if (collision.gameObject.tag == "Player" && !(curPlayerController == null))
        {
            curPlayerController.health -= 7;
            curPlayerController.StartHurtDelay();
        }
        Projectile curProjectile = collision.GetComponent<Projectile>();
        if (collision.gameObject.tag == "PlayerProjectile")
        {
            ps.Play(OnHitSound);
            health--;
            Destroy(collision.gameObject);
        }
    }
}
