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
        

    }

    // Update is called once per frame
   
}
