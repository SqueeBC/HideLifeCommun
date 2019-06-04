using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using trucs_perso;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Tuto_retour : MonoBehaviour
{
    private Text message;
    private bool trigger;

    void Start()
    {
        trigger = false;
        message = GameObject.Find("message").GetComponent<Text>();

    }



    private void OnTriggerEnter(Collider other)
    {
        GameObject.Find("TUTO check").SetActive(false);

        if (PlayerPrefs.GetString("language") == "français")
        {
            message.fontSize = 40;
            message.text = "Bien joué ! Appuyez sur X pour mettre fin au tutoriel.";
        }

        else
        {
            message.fontSize = 40;
            message.text = "Well done ! Use the key X in order to end the tutorial.";
        }

        trigger = true;
        
     
    
    }

    private void Update()
    {
        if(trigger&&Input.GetKeyDown(KeyCode.X))
            SceneManager.LoadScene(1);

    }
}