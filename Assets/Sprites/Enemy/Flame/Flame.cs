using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerController curPlayerController = collision.gameObject.GetComponent<PlayerController>();
            curPlayerController.health -= 7;
            curPlayerController.stunCharacter = true;
        }
    }
}
