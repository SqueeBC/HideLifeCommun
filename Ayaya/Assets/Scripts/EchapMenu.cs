using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using trucs_perso;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EchapMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    private GameObject player;
    private GameObject réticule;
    public GameObject OptionMenu;
    public GameObject pauseMenu;
    public Text ResumeText;
    public Text QuitText;
   

    void Start()
    {
        réticule = GameObject.Find("Réticule");

      
                foreach (GameObject _player in GameObject.FindGameObjectsWithTag("Player"))
                {
                    if (_player.GetComponent<Player>().isLocalPlayer)
                        player = _player;
                }
              
                
           
        
    }

    void Update()
    {
      
        if(player==null||!player.GetComponent<Player>().isLocalPlayer)
            Start();  
        
        Debug.Log(player.name);
        if (Input.GetButtonDown("Cancel")&&(player!=null||player.active))
        {
            if (GameIsPaused)
            {
                Resume();
               
            }
            else
            {

                Pause();
              
            }

          

        }
        ChangeLanguage();
            
    }

    public void Resume()
    {    pauseMenu.SetActive(false);
        OptionMenu.SetActive(false);
        GameIsPaused = false; 
        réticule.SetActive(true);
        player.GetComponent<PlayerControler>().enabled = true;
        player.GetComponent<PlayerShoot>().enabled = true;
        player.GetComponent<PlayerMotor>().enabled = true;
    }
        
    void Pause()
    {  
        pauseMenu.SetActive(true);
        GameIsPaused = true;
        réticule.SetActive(false);
        player.GetComponent<PlayerControler>().enabled = false;
        player.GetComponent<PlayerMotor>().enabled = false;
        player.GetComponent<PlayerShoot>().enabled = false;
        player.GetComponent<PlayerShoot>().shotaudio.Pause(); 
        //empêche le joueur de bouger et tirer la camera pendant la pause
    }

    public void OptionMenuIG()
    {
        pauseMenu.SetActive(false);
        OptionMenu.SetActive(true);
    }

    public void Echap()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }



    public void ChangeLanguage()
    {
        if (PlayerPrefs.GetString("language") == "français")

        {
            ResumeText.text = "REPRENDRE";

            QuitText.text = "QUITTER";
        }
        else
        {
            ResumeText.text = "RESUME";

            QuitText.text = "QUIT";
            
        }

    }
    
    
    
}