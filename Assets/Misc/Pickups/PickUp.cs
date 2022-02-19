using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    enum CollectableType
    {
        POWERUP,
        SCORE,
        HEALTH,
        AMMO
    }

    [SerializeField] CollectableType curCollectable;
    [SerializeField] int Value;
    public int ScoreValue;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerController curPlayerController = collision.gameObject.GetComponent<PlayerController>();

            switch (curCollectable)
            {
                case CollectableType.POWERUP:
                    curPlayerController.StartJumpForceChange();
                    break;
                case CollectableType.HEALTH:
                    curPlayerController.health += Value;
                    break;
                case CollectableType.AMMO:
                    curPlayerController.ammo += Value;
                    break;
                case CollectableType.SCORE:
                    curPlayerController.score += ScoreValue;
                    break;
            }
            Destroy(this.gameObject);
        }
    }
}
