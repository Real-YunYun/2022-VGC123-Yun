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
    public int ScoreValue;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerController curPlayerController = collision.gameObject.GetComponent<PlayerController>();

            switch (curCollectable)
            {
                case CollectableType.POWERUP:
                    curPlayerController.StartJumpForceChange();
                    curPlayerController.score++;
                    break;
                case CollectableType.HEALTH:
                    curPlayerController.lives++;
                    break;
                case CollectableType.AMMO:
                    break;
                case CollectableType.SCORE:
                    curPlayerController.score += ScoreValue;
                    break;
            }
            Destroy(gameObject);
        }
    }
}
