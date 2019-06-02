using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class multipj1 : MonoBehaviour
{
    public GameObject spawnPoint10;
    public GameObject j1;
    private bool ok = false;
    private Camera cam;
    
    // Start is called before the first frame update
    void Start()
    {
        if (!ok)
        {
            GameObject joueur = (GameObject) Instantiate(j1, spawnPoint10.transform.position, Quaternion.Euler(0.0f,0.0f,0.0f));
            joueur.GetComponent<SetUpPJ>().manualSpawn = true;
            NetworkServer.Spawn(joueur);
            Debug.Log("azerty tu existeeeee");
            cam = Camera.main;
            if (cam != null)
                cam.gameObject.SetActive(false);
        }
        ok = true;
    }
}