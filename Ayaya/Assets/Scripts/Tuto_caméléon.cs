using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using trucs_perso;
using UnityEngine;
using UnityEngine.UI;

public class Tuto_caméléon : MonoBehaviour
{
    private Text message;
    private InputManager _inputManager;
    private bool trigger;

    void Start()
    {
        trigger = false;
        _inputManager =  GameObject.Find("InputManager").GetComponent<InputManager>();
        message = GameObject.Find("message").GetComponent<Text>();

    }
    private void OnTriggerEnter( Collider other)
    {
     
      
        if (other.transform.parent.transform.parent.GetComponent<Hunter>()!=null&&!trigger)
        {    
            Destroy(other.transform.parent.transform.parent.GetComponent<Player>());
            other.transform.parent.transform.parent.gameObject.AddComponent<Prop>();
            
        }
        trigger = true;



    }

    private void Update()
    {
        if(trigger)
        {
            if (PlayerPrefs.GetString("language") == "français")
            {
                message.fontSize = 23;
                message.text = "Les caméléons ont la capacité de se transformer en objet avec la touche " +_inputManager.transfotext.text+ " .Utilisez celle-ci pour atteindre la fin du tutoriel.";
            }

            else
            {
                message.fontSize = 23;
                message.text = "Chameleons have the ability to transform into items thanks to the key "+_inputManager.transfotext.text+ ". Use it in order to reach the end.";
            }
            
        }
        

    }
}