using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public bool verbose = false;

    SpriteRenderer sr;
    Animator anim;

    public Transform spawnPointLeft;
    public Transform spawnPointRight;
        
    public float projectileSpeed;
    public Projectile projectilePreFab;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        if (projectileSpeed <- 0) projectileSpeed = 7.0f;

        if (!spawnPointLeft || !spawnPointRight || !projectilePreFab)
        {
            if (verbose) Debug.Log("Inspector Values Not Set...");
        }

    }

    private void Update()
    {
         if (Input.GetButtonDown("Fire1"))
        {
            FireProjectile();
        }
      
    }

    public void FireProjectile()
    {
        if (!sr.flipX)
        {
            Debug.Log(projectileSpeed);
            Projectile temp = Instantiate(projectilePreFab, spawnPointLeft.position, spawnPointLeft.rotation);
            temp.speed = projectileSpeed * -1;
        }
        else
        {
            Debug.Log(projectileSpeed);
            Projectile temp = Instantiate(projectilePreFab, spawnPointRight.position, spawnPointRight.rotation);
        }
    }
}