using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private bool tutorialdone = false;
    public GameObject tutorialwarning;
    private Button Play; 
    private Text SettingsText;
    private Text PlayText;
    private Text QuitText;
    private Text TutorialText;
   
    private void Start()
    {
        Play = GameObject.Find("Play").GetComponent<Button>();
        PlayText = Play.GetComponentInChildren<Text>();
        SettingsText = GameObject.Find("Settings").GetComponent<Button>().GetComponentInChildren<Text>();
        QuitText =  GameObject.Find("Quit").GetComponent<Button>().GetComponentInChildren<Text>();
        TutorialText=GameObject.Find("Tutorial").GetComponent<Button>().GetComponentInChildren<Text>();
        if (PlayerPrefs.GetString("language") == "français")
        {
            PlayText.text = "JOUER";
            SettingsText.text = "OPTIONS";
            QuitText.text = "QUITTER";
            TutorialText.text = "TUTORIEL";
        }
        else
        {
            PlayText.text = "PLAY";
            SettingsText.text = "SETTINGS";
            QuitText.text = "QUIT";
            TutorialText.text = "TUTORIAL";
        }
    }

    public void PlayGame()
    {
        if (tutorialdone == false)
        {
            tutorialdone = true;
            tutorialwarning.SetActive(true);
            Play.GetComponent<Image>().color = new Color32(255, 1, 1,255);
        }
            else
        {    
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
   
    public void GoToOptions()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);    
    }

    public void GoToTutorial()
    { SceneManager.LoadScene(6); 
        tutorialdone = true;
        

    }   
    public void Leave()
    {
        
    }    
}
