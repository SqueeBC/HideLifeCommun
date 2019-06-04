using System.Collections;
using System.Collections.Generic;
using trucs_perso;
using UnityEngine;
using UnityEngine.Networking;

public class Prop : Player
{  
    [SerializeField]
    private Camera camera;
    
    [SerializeField]
    private LayerMask mask;
 
    public string propSize;
    private void Start()
    {   mask.value  = 2359;
        id = GetComponent<NetworkIdentity>().netId.ToString();
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        gameObject.transform.GetChild(0).GetChild(0).gameObject.SetActive(true); 
        gameObject.transform.GetChild(0).GetChild(1).gameObject.SetActive(true); 
        gameObject.transform.GetChild(1).gameObject.SetActive(false); 
        Destroy(gameObject.GetComponent<PlayerShoot>());
        if(isLocalPlayer)
        camera = gameObject.transform.GetChild(0).GetComponentInChildren<Camera>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        maxHP = 100;
        currentHP = maxHP;
        
    }
    
  
    
    private  void Update()
    { Debug.Log(mask.value);
        if (_time > -1 && gameObject.GetComponent<Collider>() != null)
        {
            if (CompareTag("Spectator"))
            {
                Debug.Log(true);
                _time -= Time.deltaTime;
            }

            if (_time <= 0)
                Destroy(gameObject.GetComponent<Collider>());
           ;

        }
        if  (Input.GetKey((KeyCode) System.Enum.Parse(typeof(KeyCode),PlayerPrefs.GetString("TransfoKey", "T")))&&!CompareTag("Spectator"))
            TransformationTest();
    }

    public void TransformationTest() //fonction provisoire pour les tests
    {
                
        RaycastHit hit;
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, 100,mask)) ;
        {
            Debug.Log(hit.collider.name);
            if (hit.collider != null&&(hit.collider.CompareTag("Big Item")||hit.collider.CompareTag("Medium Item")||hit.collider.CompareTag("Small Item")))
            {
                       Debug.Log("transfo");
                Destroy(transform.GetChild(0).Find("caméléon").gameObject);
                        
                GameObject gameObject = Instantiate(hit.collider.transform.gameObject);
                if(gameObject.GetComponent<Rigidbody>()!=null)
                    Destroy(gameObject.GetComponent<Rigidbody>());
                gameObject.name = "caméléon";
                gameObject.transform.localPosition = transform.localPosition;
                gameObject.transform.parent = transform.GetChild(0);
                if (gameObject.GetComponent<Collider>() != null)
                {
                    Destroy(this.gameObject.GetComponent<Collider>());
                        
                            
                }

                gameObject.layer = 10;
                gameObject.SetActive(true);
                       
                propSize = hit.collider.tag;
                ChangeHP();
            }
        }
    }
    private void ChangeHP()
    {
        switch (propSize)
        {
            case "Small Item":
                currentHP = currentHP * 50 / maxHP;
                maxHP = 50;
                GetComponent<PlayerControler>().speed = 6;
             
                break;
                    
            case "Medium Item":
                currentHP = currentHP * 100 / maxHP;
                GetComponent<PlayerControler>().speed = 4;
                maxHP = 100;
                break;
            case "Big Item":
                currentHP = currentHP * 200 / maxHP;
                GetComponent<PlayerControler>().speed = 3.5f;
                maxHP = 200;
                break;
        }
    }
              
}