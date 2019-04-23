using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Sensitivity : MonoBehaviour
{
    public Text SensibilityPercentage;
     public Slider slider;
    private void Start()
    {
       
        if (PlayerPrefs.HasKey("sensibilité"))
        {
                SensibilityPercentage.text = Mathf.RoundToInt(PlayerPrefs.GetFloat("sensibilité",0) * 100) + "%";
                              slider.value = ( PlayerPrefs.GetFloat("sensibilité",0));
    }
        
}

    private void Update()
    {
       SensibilityPercentageUpdate(slider.value);
    }


    public void  SensibilityPercentageUpdate(float value)
   {
      PlayerPrefs.SetFloat("sensibilité",value);
      SensibilityPercentage.text = Mathf.RoundToInt( PlayerPrefs.GetFloat("sensibilité") * 100) + "%";
   }

   

 

}
