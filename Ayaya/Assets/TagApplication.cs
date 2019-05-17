using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagApplication : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int i = 0;
        while(i<transform.childCount)
        {
            transform.GetChild(i).tag = "Ground";
            i++;
        }

        i = 0;
        GameObject items =  GameObject.Find("Items");
        while(i<items.transform.childCount)
        {
          
            items.transform.GetChild(i).tag = "Item";
            
            i++;
            
        }
        i = 0;
        GameObject Small =  GameObject.Find("Small Items");
        while(i<Small.transform.childCount)
        {
          
            Small.transform.GetChild(i).tag = "Small Item";
            
            i++;
            
        }
        i = 0;
        GameObject Medium =  GameObject.Find("Small Items");
        while(i<Medium.transform.childCount)
        {
          
            Medium.transform.GetChild(i).tag = "Medium Item";
            
            i++;
            
        }
        i = 0;
        GameObject Big =  GameObject.Find("Big Items");
        while(i<Big.transform.childCount)
        {
          
            Big.transform.GetChild(i).tag = "Big Item";
            
            i++;
            
        }
        

        
        

    }

    // Update is called once per frame
   
}
