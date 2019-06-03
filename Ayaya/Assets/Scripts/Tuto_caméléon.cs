using System.Collections;
using System.Collections.Generic;
using trucs_perso;
using UnityEngine;

public class Tuto_caméléon : MonoBehaviour
{
    
    private void OnTriggerEnter( Collider other)
    {
     
        if (other.transform.parent.transform.parent.GetComponent<Hunter>()!=null)
        {    
            Destroy(other.transform.parent.transform.parent.GetComponent<Player>());
            other.transform.parent.transform.parent.gameObject.AddComponent<Prop>();
            Destroy(this);
        }
        

    }
}
