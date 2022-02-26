using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LivesRemaining : MonoBehaviour
{
    private int callToPlayerController;
    [SerializeField] Sprite Lives0;
    [SerializeField] Sprite Lives1;
    [SerializeField] Sprite Lives2;
    [SerializeField] Sprite Lives3;
    Image img;

    void Start()
    {
        img = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        callToPlayerController = PlayerController._lives;
        if (callToPlayerController == 3) img.sprite = Lives3;
        else if (callToPlayerController == 2) img.sprite = Lives2;
        else if (callToPlayerController == 1) img.sprite = Lives1;
        else img.sprite = Lives0;
    }
}
