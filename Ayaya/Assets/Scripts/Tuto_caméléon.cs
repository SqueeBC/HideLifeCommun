using System.Collections;
using System.Collections.Generic;
using trucs_perso;
using UnityEngine;

public class Tuto_caméléon : MonoBehaviour
{
    
    private void OnTriggerEnter( Collider other)
    {
        Debug.Log(other.name);
        if (other.GetComponent<Hunter>())
        {    
            Destroy(other.gameObject.GetComponent<Player>());
            other.gameObject.AddComponent<Prop>();
        }

    }
}
