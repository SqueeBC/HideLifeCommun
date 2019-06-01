using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagApplication : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int i = 0;
      GameObject Small =  GameObject.Find("PETITS OBJETS");
        while(i<Small.transform.childCount)
        {
          
            Small.transform.GetChild(i).tag = "Small Item";
            
            i++;
            
        }
        i = 0;
        GameObject Medium =  GameObject.Find("OBJETS MOYEN");
        while(i<Medium.transform.childCount)
        {
          
            Medium.transform.GetChild(i).tag = "Medium Item";
            
            i++;
            
        }
        i = 0;
        GameObject Big =  GameObject.Find("GRAND OBJETS");
        while(i<Big.transform.childCount)
        {
          
            Big.transform.GetChild(i).tag = "Big Item";
            
            i++;
            
        }

    }

   
}
