using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Animator))]

public class PlayerFire : MonoBehaviour
{
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
    }

    private void Update()
    {
         if (Input.GetButtonDown("Fire1") && Time.timeScale == 1)
        {
            PlayerController curPlayerController = gameObject.GetComponent<PlayerController>();
            if (curPlayerController.ammo != 0 && !(curPlayerController.ammo < 0))
            {
                FireProjectile();
            }
        }
      
    }

    public void FireProjectile()
    {
        if (!sr.flipX)
        {
            Projectile temp = Instantiate(projectilePreFab, spawnPointLeft.position, spawnPointLeft.rotation);
            temp.speed = projectileSpeed * -1;
        }
        else
        {
            Projectile temp = Instantiate(projectilePreFab, spawnPointRight.position, spawnPointRight.rotation);
        }
    }
}
