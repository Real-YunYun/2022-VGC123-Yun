using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPickUps : MonoBehaviour
{

    public PickUp[] pickupsPreFabArray;

    // Start is called before the first frame update
    void Start()
    {
        int randValue = Random.Range(0,7);
        Instantiate(pickupsPreFabArray[randValue], transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
