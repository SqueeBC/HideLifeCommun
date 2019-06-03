using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Tuto_retour : MonoBehaviour
{ private Text message;
    private float time = 5f;
    private bool trigger;
    
    void Start()
    {trigger = false;
        message = GameObject.Find("message").GetComponent<Text>();
                            
    }



    private void OnTriggerEnter(Collider other)
    {  
        if (trigger&&time>=0)
        {
            if (PlayerPrefs.GetString("language") == "français")
            {
                message.fontSize = 40;
                message.text = "Bien joué ! Retour au menu dans " + Mathf.RoundToInt(time) + " secondes.";
            }

            else
            {  message.fontSize = 40;
                message.text = "Well done ! Coming back to the menu in " + Mathf.RoundToInt(time )+ " seconds.";
            }

            time -= 1;
        }
        SceneManager.LoadScene(1);
    }

       
    }

