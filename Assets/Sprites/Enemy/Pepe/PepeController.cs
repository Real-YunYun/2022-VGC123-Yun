using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Collider2D))]

public class PepeController : MonoBehaviour
{
    Rigidbody2D rb;
    Collider2D c2;
    SpriteRenderer sr;

    [SerializeField] LayerMask isStopWallLayer;
    [SerializeField] LayerMask isPlayerLayer;

    public Vector2 speed;
    public bool flipped = false;
    public bool inLayerMask = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        c2 = GetComponent<Collider2D>();
        sr = GetComponent<SpriteRenderer>();
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
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerController curPlayerController = collision.gameObject.GetComponent<PlayerController>();
            curPlayerController.health-=7;
            curPlayerController.isHurt = true;
        }
        if (collision.gameObject.tag == "PlayerProjectile")
        {
            Destroy(this.gameObject);
        }
    }
}
