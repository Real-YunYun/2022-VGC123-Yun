using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireArea : MonoBehaviour
{
    [SerializeField] Manbu curManbu;
    [SerializeField] LayerMask isPlayerLayer;
    Collider2D FireZone;
    // Start is called before the first frame update
    void Start()
    {
        FireZone = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (FireZone.IsTouchingLayers(isPlayerLayer)) curManbu.attack = true;
        if (!(curManbu == null)) if (!(FireZone.IsTouchingLayers(isPlayerLayer))) curManbu.attack = false;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            curManbu.pos = collision.gameObject.transform.position;
            curManbu.attack = true;
        }
        else curManbu.attack = false;
    }
}
