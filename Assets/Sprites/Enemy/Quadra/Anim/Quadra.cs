using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quadra : MonoBehaviour
{

    SpriteRenderer sr;
    Animator anim;

    [SerializeField] GameObject flame;
    [SerializeField] Transform spawnPoint1;
    [SerializeField] Transform spawnPoint2;
    [SerializeField] Transform spawnPoint3;
    [SerializeField] Transform spawnPoint4;

    public int idleTime = 1000;

    public bool attack = false;

    public int currentTime = 1000;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (attack && currentTime <= 0)
        {
            currentTime = idleTime;
            attack = false;
        }

        if (!attack && currentTime <= 0)
        {
            currentTime = 1000;
            attack = true;
        }
        currentTime--;
        anim.SetBool("Attack", attack);
    }

    public void StartFlame()
    {
        GameObject temp1Flame = flame;
        GameObject temp2Flame = flame;
        GameObject temp3Flame = flame;
        GameObject temp4Flame = flame;

        Instantiate(temp1Flame, spawnPoint1.position, spawnPoint1.rotation);
        Instantiate(temp2Flame, spawnPoint2.position, spawnPoint2.rotation);
        Instantiate(temp3Flame, spawnPoint3.position, spawnPoint3.rotation);
        Instantiate(temp4Flame, spawnPoint4.position, spawnPoint4.rotation);

        Destroy(temp1Flame.gameObject, 0.5f);
        Destroy(temp2Flame.gameObject, 0.5f);
        Destroy(temp3Flame.gameObject, 0.5f);
        Destroy(temp4Flame.gameObject, 0.5f);
    }
}
