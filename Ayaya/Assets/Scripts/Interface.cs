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
    
    public Text Staminatext;
    public Text HPtext;
    public Image HPbar;
    public Text AmmoText;
    public Text Cooldownleft;
    public GameObject ReloadingText;
    public PlayerControler playerControler;
    public PlayerShoot Localplayershoot;
    public Player player;
    public GameObject hitmarker;
    private float time;
    [SerializeField] 
    private Text spect;
    private void Start()
    {FindPlayer();
        
    }

    public void FindPlayer()
    {
        foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("Player"))
        {
           
            if (gameObject.GetComponent<Player>().isLocalPlayer)
                player = gameObject.GetComponent<Player>();
        }

        
        Localplayershoot = player.GetComponent<PlayerShoot>(); //a modifier pour le multi, avec GetComponent<Newwork.Behaviour>().IsLocalPlayer
        playerControler= (PlayerControler) player.GetComponent("PlayerControler");
    }
    // Update is called once per frame
    void Update()
    {

        if (player==null)
        {
            FindPlayer();
        }
        
        if (time > 0)
            time -= Time.deltaTime;
        if(hitmarker.active&&time<0)
            hitmarker.SetActive(false);
        
        HPbar.fillAmount =1-(float)player.currentHP/player.maxHP;
       
        Staminabar.fillAmount =1-  playerControler.stamina/100;        
        StaminaText();
        CoolDownText();
        AmmoUpdate();
        Reloading();
        HPText();
        if (player.CompareTag("Spectator"))
            Spect();
    }

    public void StaminaText()
    {
        if (PlayerPrefs.GetString("language") == "français")
            Staminatext.text = "ENDURANCE:" + Mathf.Round(playerControler.stamina) + "%";
        else
        {


            Staminatext.text = "STAMINA:" + Mathf.Round(playerControler.stamina) + "%";
        }
    }
    
    public void HPText()
    {
            Debug.Log(player.name);
        if (PlayerPrefs.GetString("language") == "français")
            HPtext.text = "PV:" + Mathf.Round(player.currentHP*((float) 100/player.maxHP)) + "%";
        else
        {

            HPtext.text = "HP:" + Mathf.Round(player.currentHP*((float)100/player.maxHP)) + "%";
        }
    }



    private void Spect()
    {      
        if (PlayerPrefs.GetString("language") == "français")
            spect.text = "Utilisez ctrl pour descendre";
        else
        {
            spect.GetComponent<Text>().text = "Use ctrl in order to go down";
        }

        ReloadingText.SetActive(true);
    }


public void Reloading()
{
       
        
if((Localplayershoot!=null)&&Localplayershoot.ReloadTime>0)
ReloadingText.SetActive(true);
else
{
ReloadingText.SetActive(false);
}
            
            
}
public void AmmoUpdate()
{
if(Localplayershoot!=null)
AmmoText.text = "AMMO:" +Localplayershoot.weapon.ammo+"/"+Localplayershoot.weapon.chargercapacity;
else

{
AmmoText.text = "";
}
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

public void ShowHitmarker()
{
time = 0.1f;
hitmarker.SetActive(true);
}


}