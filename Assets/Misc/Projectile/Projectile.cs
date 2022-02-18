using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float speed;
    public float lifetime;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (lifetime <= 0) lifetime = 1.0f;
        rb.velocity = new Vector2(speed, 0);
        Destroy(gameObject, lifetime);
    }
   
}
