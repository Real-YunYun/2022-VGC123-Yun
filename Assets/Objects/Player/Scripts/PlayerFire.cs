using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(SpriteRenderer), typeof(Animator), typeof(PlayerSounds))]

public class PlayerFire : MonoBehaviour
{
    SpriteRenderer sr;
    Animator anim;
    PlayerSounds ps;

    public Transform spawnPointLeft;
    public Transform spawnPointRight;

    public float projectileSpeed;
    public Projectile projectilePreFab;

    [SerializeField] AudioClip PlayerFireSound;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        ps = GetComponent<PlayerSounds>();

        if (projectileSpeed <- 0) projectileSpeed = 7.0f;
    }

    private void Update()
    {
         if (Input.GetButtonDown("Fire1") && Time.timeScale == 1)
        {
            PlayerController curPlayerController = gameObject.GetComponent<PlayerController>();
            if (curPlayerController.ammo != 0 && !(curPlayerController.ammo < 0) && !curPlayerController.stunCharacter) FireProjectile();
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
        ps.Play(PlayerFireSound);
    }
}
