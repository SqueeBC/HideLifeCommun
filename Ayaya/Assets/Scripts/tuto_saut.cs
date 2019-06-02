using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class tuto_saut : MonoBehaviour
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
            message.text = "Pour sauter, utilisez "  +  _inputManager.jumptext.text;
            message.fontSize = 40;
        }

        else
        {message.fontSize = 40;
            message.text = "To jump, use " + _inputManager.jumptext.text;
        }
    }

}