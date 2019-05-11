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
       
    }

    // Update is called once per frame
   
}
