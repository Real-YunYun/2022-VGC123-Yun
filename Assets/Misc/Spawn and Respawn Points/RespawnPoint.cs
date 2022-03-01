using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    [SerializeField] GameObject player;
    Transform PlayerTransform;
    // Start is called before the first frame update
    void Start()
    {
        PlayerTransform = player.GetComponent<Transform>();
        PlayerTransform.position = this.gameObject.transform.position;
        PlayerTransform.rotation = this.gameObject.transform.rotation;
    }
}
