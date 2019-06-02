using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class multipj1 : MonoBehaviour
{
    public GameObject spawnPoint9;
    public GameObject j1;
    public GameObject actu;
    
    // Start is called before the first frame update
    void Start()
    {
        actu = j1;
        actu.transform.position = spawnPoint9.transform.position;
    }
}
