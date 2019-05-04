using System;
using System.Collections;
using System.Collections.Generic;
using trucs_perso;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Interface : MonoBehaviour
{
    public GameObject CooldownReminder;
    public Image Staminabar;
    public GameObject Player;
    public Text Staminatext;
    public Text HPtext;
    public Image HPbar;
    public Text AmmoText;
    public Text Cooldownleft;
    public GameObject ReloadingText;
    public PlayerControler playerControler;
    public PlayerShoot Localplayershoot;
    public Player player;
    private void Start()
    {
       Update();
        
    }

    public void FindPlayer()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        Localplayershoot = player.GetComponent<PlayerShoot>(); //a modifier pour le multi, avec GetComponent<Newwork.Behaviour>().IsLocalPlayer
        playerControler= (PlayerControler) player.GetComponent("PlayerControler");
    }
    // Update is called once per frame
    void Update()
    {

        while (player==null)
        {
            FindPlayer();
        }

        HPbar.fillAmount =1-(float)player.currentHP/100;
       
        Staminabar.fillAmount =1-  playerControler.stamina/100;        
        StaminaText();
        CoolDownText();
        AmmoUpdate();
        Reloading();
        HPText();
    }

    public void StaminaText()
         {
             Staminatext.text = "STAMINA:"+Mathf.Round(playerControler.stamina)  + "%";
         }
    
    public void HPText()
    {
        HPtext.text = "HP:"+Mathf.Round(player.currentHP)  + "%";
    }

    public void Reloading()
    {
        if(Localplayershoot.ReloadTime>0)
            ReloadingText.SetActive(true);
        else
        {
            ReloadingText.SetActive(false);
        }
            
            
    }
    public void AmmoUpdate()
    {
        
        AmmoText.text = "AMMO:" +Localplayershoot.weapon.ammo+"/"+Localplayershoot.weapon.chargercapacity;
    }

    public void CoolDownText()
    {
        if (playerControler.SprintCooldown > 0)
        {
            CooldownReminder.SetActive(true);
            Cooldownleft.text = Math.Round(playerControler.SprintCooldown, 1) + "S";
        }
        else
        {CooldownReminder.SetActive(false);
            
        }

    }
}
