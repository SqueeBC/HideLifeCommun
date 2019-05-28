using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class Tuto_sprint : MonoBehaviour
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
            message.text = "Le spint vous permet d'aller beaucoup plus vite, mais son usage est limité; quand votre stamina atteint 0 vous serez dans l'incapacité de sprinter. \n \n Pour courir, utilisez la touche "+
        _inputManager.runtext.text + " puis sautez pour atteindre la suite du toturiel";}

        else
        {  message.fontSize = 30;
            message.text = "Sprint makes you way more faster, but cost stamina. When it reaches 0, you won't be able to sprint."
                + "in order to sprint, use " +     _inputManager.runtext.text + "then jump to reach the next of the tutorial";
        }
    }
}