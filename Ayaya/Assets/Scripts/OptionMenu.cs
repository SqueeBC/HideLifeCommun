using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour
{

    
    public void Echap()
    {
        if (Input.GetButton("Cancel"))
        {
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + -2);

        }
    }

     
   
    private void Update()
    {
        
        Echap();
    }
}
