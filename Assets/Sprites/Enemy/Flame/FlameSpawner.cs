using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameSpawner : MonoBehaviour
{
    Animator anim;
    SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
