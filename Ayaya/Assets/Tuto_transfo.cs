using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class Tuto_transfo : MonoBehaviour
{
    private InputManager _inputManager;
    private Text message;


    void Start()
    {
        message = GameObject.Find("message").GetComponent<Text>();
        _inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
    }

    private void
        OnTriggerStay(
            Collider other) //on utilise stay au lieu de enter pour que s'il y'ait changement de langue la traduction soit instantannée
    {
        if (PlayerPrefs.GetString("language") == "français")
        {
            message.fontSize = 30;
            message.text = "Les props peuvent se transformer en objet en utilisant la touche " +
                           _inputManager.transfotext.text +
                           ". Transformez vous en un petit objet pour pouvoir progresser.";
        }

        else
            {
                message.fontSize = 30;
                message.text =
                    "Sprint makes you way more faster, but cost stamina. When it reaches 0, you won't be able to sprint."
                    + "in order to sprint, use " + _inputManager.runtext.text +
                    "then jump to reach the next of the tutorial";
            }
        }
    }

