using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPickUps : MonoBehaviour
{
    public bool spawnOnUpdate = true;
    public PickUp[] pickupsPreFabArray;

    // Start is called before the first frame update
    void Start()
    {
        int randValue = Random.Range(0,3);
        if (randValue != 3 && !spawnOnUpdate)
        {
            Instantiate(pickupsPreFabArray[randValue + 7], transform.position, transform.rotation);
        }
        else if (randValue == 3)
        {
            randValue = Random.Range(0,7);
            Instantiate(pickupsPreFabArray[randValue], transform.position, transform.rotation);
        }
    }

    public void spawnPickUpOnUpdate()
    {
        int randValue = Random.Range(0, 3);
        if (randValue != 3)
        {
            Instantiate(pickupsPreFabArray[randValue + 7], transform.position, transform.rotation);
        }
        else if (randValue == 3)
        {
            randValue = Random.Range(0, 7);
            Instantiate(pickupsPreFabArray[randValue], transform.position, transform.rotation);
        }
    }
}
