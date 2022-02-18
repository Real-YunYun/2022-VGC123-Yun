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

    public void SpawnFlames()
    {
        GameObject temp = flame;
        if (attack)
        {
            temp = Instantiate(flame, spawnPoint1.position, spawnPoint1.rotation);
            Animator flameAnim = flame.GetComponent<Animator>();
            flameAnim.SetBool("Loop", true);
        } else if (!attack)
        {
            Destroy(temp);
        }
    }
}
