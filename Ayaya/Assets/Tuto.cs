using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class Tuto : MonoBehaviour 
{
    private InputManager _inputManager;
    private Text message;
   
    void Start()
    {
        message = GameObject.Find("message").GetComponent<Text>();
        _inputManager =  GameObject.Find("InputManager").GetComponent<InputManager>();
    }

    private void OnTriggerStay(Collider other) //on utilise stay au lieu de enter pour que s'il y'ait changement de langue la traduction soit instantannée
    {
        if (PlayerPrefs.GetString("language") == "français")
        {
            message.fontSize = 40;

            message.text = "Pour vous deplacer, utilisez les touches "  + _inputManager.forwardtext.text + " " +_inputManager.lefttext.text +" " + _inputManager.backwardtext.text  + " " +_inputManager.righttext.text;
        }
        else
        {            message.fontSize = 40;

            message.text = "In order to move, use the keys"   + _inputManager.forwardtext.text + " " +_inputManager.lefttext.text +" " + _inputManager.backwardtext.text  + " " +_inputManager.righttext.text;
        }
    }


}
