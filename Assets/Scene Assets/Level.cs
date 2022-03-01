using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public int level;
    public int StartingLives;
    public Transform spawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.state.lives = StartingLives;
        GameManager.state.SpawnPlayer(spawnPoint);
        GameManager.state.currentLevel = this;
    }


}