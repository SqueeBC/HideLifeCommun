using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    
    public GameObject tutorialwarning;
    private Button Play; 
    private Text SettingsText;
    private Text PlayText;
    private Text QuitText;
    private Text TutorialText;
   
    private void Start()
    {
        Cursor.visible = true;
        GameObject.Find("network manager").SetActive(false);
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
        if (PlayerPrefs.GetString("tuto","no") == "no")
        {
            PlayerPrefs.SetString("tuto", "yes");
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
        PlayerPrefs.SetString("tuto", "yes");
        

    }   
    public void Leave()
    {
        
    }    
}
