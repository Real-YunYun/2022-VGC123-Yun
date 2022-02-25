using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manbu : MonoBehaviour
{
    [SerializeField] PlayerController curPlayerController;
    [SerializeField] FireArea FireZone;
    [SerializeField] Transform projectileSpawnPoint;
    [SerializeField] ManbuProjectile projectile;

    SpriteRenderer sr;
    Animator anim;

    public bool attack = false;
    public Vector2 pos;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>(); 
        anim = GetComponent<Animator>(); 
    }

    void Update()
    {
        if (attack)
        {
            anim.SetBool("Attack", true);
        }
        else anim.SetBool("Attack", false);
    }

    public void ManbuAttack()
    {
        if (gameObject.transform.position.x > curPlayerController.transform.position.x) sr.flipX = false;
        if (gameObject.transform.position.x < curPlayerController.transform.position.x) sr.flipX = true;
        ManbuProjectile temp = Instantiate(projectile, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
        temp.PlayerPos = curPlayerController.transform.position;
        attack = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerProjectile")
        {
            Destroy(gameObject);
        }
    }
}
