using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PickUp : MonoBehaviour
{
    enum CollectableType
    {
        POWERUP,
        SCORE,
        HEALTH,
        AMMO
    }

    PlayerSounds ps;
    [SerializeField] AudioClip Up1;
    [SerializeField] AudioClip PointTally;
    
    [SerializeField] CollectableType curCollectable;
    [SerializeField] int Value;
    public int ScoreValue;

    private void Start()
    {
        ps = GetComponent<PlayerSounds>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && !(collision.gameObject.GetComponent<PlayerController>() == null))
        {

            switch (curCollectable)
            {
                case CollectableType.POWERUP:
                    collision.gameObject.GetComponent<PlayerController>().StartJumpForceChange();
                    collision.gameObject.GetComponent<PlayerController>().PlayPickUpSound(PointTally);
                    break;
                case CollectableType.HEALTH:
                    collision.gameObject.GetComponent<PlayerController>().health += Value;
                    collision.gameObject.GetComponent<PlayerController>().PlayPickUpSound(PointTally);
                    break;
                case CollectableType.AMMO:
                    collision.gameObject.GetComponent<PlayerController>().ammo += Value;
                    collision.gameObject.GetComponent<PlayerController>().PlayPickUpSound(PointTally);
                    break;
                case CollectableType.SCORE:
                    GameManager.state.score += ScoreValue;
                    collision.gameObject.GetComponent<PlayerController>().PlayPickUpSound(PointTally);
                    break;
            }
            Destroy(this.gameObject);
        }
    }
}
