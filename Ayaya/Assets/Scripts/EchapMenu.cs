using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EchapMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    private GameObject player;
    private GameObject réticule;
    public GameObject OptionMenu;
    public GameObject pauseMenu;
    
   

    void Start()
    {
        réticule = GameObject.Find("Réticule");
        player = GameObject.Find("Player");
    }

    void Update()
    {
        
        if(player==null)
            Start();  
        
    Debug.Log(player);
        if (Input.GetButtonDown("Cancel"))
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

     
        
    
    
    
    }

