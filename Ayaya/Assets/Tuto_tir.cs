using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class Tuto_tir : MonoBehaviour
{
    private InputManager _inputManager;
    private Text message;


    void Start()
    {
        message = GameObject.Find("message").GetComponent<Text>();
        _inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
    }
    
    private void OnTriggerStay( Collider other) //on utilise stay au lieu de enter pour que s'il y'ait changement de langue la traduction soit instantannée
    {
        if (PlayerPrefs.GetString("language") == "français")
        {
            message.fontSize = 30;
            message.text = "Utilisez le clique gauche pour tirer et le clique droit pour viser. Détruisez les cibles pour acceder à la suite.";
        }

        else
        {  message.fontSize = 30;
            message.text =   "Use the left click in order to fire and right click in order to aim. Destroy the targets in order to have access to the next of the tutorial.";
        }
    }
}