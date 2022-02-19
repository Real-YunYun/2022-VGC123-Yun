using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameSpawner: MonoBehaviour
{
    Transform tr;
    Animator anim;
    SpriteRenderer sr;

    public bool spawned = false;
    [SerializeField] GameObject curFlame;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        tr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Quadra parent = this.transform.parent.GetComponent<Quadra>();
        if (parent.attack && !spawned)
        {
            spawned = true;
            GameObject clone = Instantiate(curFlame, tr.position, tr.rotation);
            Destroy(clone, 5f);
        }
        else if (!parent.attack && spawned)
        {
            spawned = false;
        }
    }
}