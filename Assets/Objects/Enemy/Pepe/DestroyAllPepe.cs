using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAllPepe : MonoBehaviour
{
    [SerializeField] PepeController pepeChild;

    // Update is called once per frame
    void Update()
    {
        if (pepeChild == null)
        {
            Destroy(this.gameObject);
        }
    }
}
