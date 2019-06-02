using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class multipj1 : MonoBehaviour
{
    public GameObject spawnPoint9;
    public GameObject j1;
    public GameObject actu;
    private bool ok = false;
    
    // Start is called before the first frame update
    void Start()
    {
        if (!ok)
        {
            NetworkServer.Spawn(j1);
            actu = j1;
            actu.transform.position = spawnPoint9.transform.position;
        }
        ok = true;
    }
}