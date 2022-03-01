using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quadra : MonoBehaviour
{

    SpriteRenderer sr;
    Animator anim;

    public bool attack = false;

    private void Start()
    {        
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("Attack", attack);
    }
    
    public void ChangeAttack()
    {
        attack = !attack;
    }
}
