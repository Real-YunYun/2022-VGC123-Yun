using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]

public class Flame : MonoBehaviour
{
    Collider2D c2;
    void Start()
    {
        c2 = GetComponent<Collider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerController curPlayerController = collision.gameObject.GetComponent<PlayerController>();
            if (!(curPlayerController == null))
            {
                curPlayerController.health -= 7;
                curPlayerController.StartHurtDelay();
            }
        }
    }
}
