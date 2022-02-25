using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class WeaponsMenu : MonoBehaviour
{
    Animator anim;
    [SerializeField] PlayerController player;
    [SerializeField] Image MenuBar1;
    [SerializeField] Image MenuBar2;
    [SerializeField] Image MenuBar3;
    [SerializeField] Image MenuBar4;
    [SerializeField] Image MenuBar5;
    [SerializeField] Image MenuBar6;
    [SerializeField] Image MenuBar7;
    [SerializeField] Image PlayerLives;
    [SerializeField] Sprite Life1;
    [SerializeField] Sprite Life2;
    [SerializeField] Sprite Life3;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tilde)) { Application.Quit(); }
        if (Input.GetKeyDown(KeyCode.Tab) && !anim.GetBool("Display"))
        {
            Time.timeScale = 0;
            anim.SetBool("Display", true);            
        }
        else if (Input.GetKeyDown(KeyCode.Tab) && anim.GetBool("Display"))
        {
            ClearUI();
            anim.SetBool("Display", false);
        }
    }

    public void DispayAnimation()
    {
        anim.Play("Display");
        MenuBar1.fillAmount = 1.0f;
        MenuBar2.fillAmount = 1.0f;
        MenuBar3.fillAmount = 1.0f;
        MenuBar4.fillAmount = 1.0f;
        MenuBar5.fillAmount = 1.0f;
        MenuBar6.fillAmount = 1.0f;
        MenuBar7.fillAmount = player.health * ( 1.0f / 28.0f );
        if (player.lives == 3) PlayerLives.sprite = Life3;
        else if (player.lives == 2) PlayerLives.sprite = Life2;
        else if (player.lives == 1) PlayerLives.sprite = Life1;
        PlayerLives.fillAmount = 1.0f; 
    }
    public void NoDisplayAnimation()
    {
        anim.Play("NoDisplay");
    }

    public void Pause()
    {
        Time.timeScale = 0;
    }
    public void UnPause()
    {
        Time.timeScale = 1;
    }

    public void ClearUI()
    {
        MenuBar1.fillAmount = 0f;
        MenuBar2.fillAmount = 0f;
        MenuBar3.fillAmount = 0f;
        MenuBar4.fillAmount = 0f;
        MenuBar5.fillAmount = 0f;
        MenuBar6.fillAmount = 0f;
        MenuBar7.fillAmount = 0f;
        PlayerLives.fillAmount = 0f;
    }

}
