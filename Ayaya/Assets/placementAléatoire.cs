using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class placementAléatoire : NetworkBehaviour
{
    public GameObject obj1;
    public Transform pos1;
    
    
    // Start is called before the first frame update
    [Server]
    void Start()
    {
        GameObject testobj1 = (GameObject) Instantiate(obj1,pos1);
        NetworkServer.Spawn(testobj1);
        Debug.Log("coiuytfdsq");
    }
}
