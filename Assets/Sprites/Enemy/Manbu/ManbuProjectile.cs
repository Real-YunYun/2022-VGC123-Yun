using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManbuProjectile : MonoBehaviour
{
    public float speed;
    public Vector3 PlayerPos;
    public float lifetime;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (lifetime <= 0) lifetime = 1.0f;
        PlayerPos.y += 0.5f;
        Vector3 tempVector = PlayerPos - gameObject.transform.position;
        rb.velocity = speed * tempVector.normalized;
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController curPlayerController = collision.gameObject.GetComponent<PlayerController>();
        if (collision.gameObject.tag == "Player" && !(curPlayerController == null))
        {
            curPlayerController.health -= 4;
            curPlayerController.StartHurtDelay();
            Destroy(this.gameObject);
        }
        if (collision.gameObject.tag == "Ground") Destroy(this.gameObject);
    }
}
