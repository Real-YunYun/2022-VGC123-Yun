using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Manbu : MonoBehaviour
{
    [SerializeField] FireArea FireZone;
    [SerializeField] Transform projectileSpawnPoint;
    [SerializeField] SpawnPickUps spawnerPickUp;
    [SerializeField] ManbuProjectile projectile;
    [SerializeField] AudioClip OnHitSound;
    [SerializeField] AudioClip OnShootSound;
    [SerializeField] AudioClip OnDeathSound;

    SpriteRenderer sr;
    Animator anim;
    PlayerSounds ps;
    
    public int health = 8;
    public bool attack = false;
    public Vector2 pos;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>(); 
        anim = GetComponent<Animator>();
        ps = GetComponent<PlayerSounds>();
    }

    void Update()
    {
        if (attack)
        {
            anim.SetBool("Attack", true);
        }
        else anim.SetBool("Attack", false);
        if (health <= 0)
        {
            ps.Play(OnDeathSound);
            spawnerPickUp.spawnPickUpOnUpdate();
            Destroy(this.gameObject);
        }
    }

    public void ManbuAttack()
    {
        sr.flipX = this.gameObject.transform.position.x > GameManager.state.MegaMan.transform.position.x;
        ManbuProjectile temp = Instantiate(projectile, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
        ps.Play(OnShootSound);
        temp.PlayerPos = GameManager.state.MegaMan.transform.position;
        attack = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerProjectile")
        {
            ps.Play(OnHitSound);
            health--;
            Destroy(collision.gameObject);
        }
    }
}
