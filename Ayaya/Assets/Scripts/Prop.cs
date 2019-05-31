using System.Collections;
using System.Collections.Generic;
using trucs_perso;
using UnityEngine;

public class Prop : Player
{  
    [SerializeField]
    private Camera camera;

    private LayerMask _layerMask = 9;
    private string propSize;
    private void Start()
    {   gameObject.transform.GetChild(1).transform.GetChild(1).gameObject.SetActive(false); 
        gameObject.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false); 
        gameObject.transform.GetChild(1).transform.GetChild(0).gameObject.SetActive(true); 
        camera = GetComponentInChildren<Camera>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        maxHP = 100;
        currentHP = maxHP;
       
    }
    
    private void Update()
    {
        if  (Input.GetKey((KeyCode) System.Enum.Parse(typeof(KeyCode),PlayerPrefs.GetString("TransfoKey", "T"))))
            TransformationTest();
    }

    public void TransformationTest() //fonction provisoire pour les tests
    {
                
        RaycastHit hit;
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, 100,_layerMask)) ;
        {
            Debug.Log(hit.collider.name);
            if (hit.collider != null)
            {
                       
                Destroy(transform.FindChild("Graphics").gameObject);
                        
                GameObject gameObject = Instantiate(hit.collider.transform.gameObject);
                if(gameObject.GetComponent<Rigidbody>()!=null)
                    Destroy(gameObject.GetComponent<Rigidbody>());
                gameObject.name = "Graphics";
                gameObject.transform.localPosition = transform.localPosition;
                gameObject.transform.parent = transform;
                if (gameObject.GetComponent<Collider>() != null)
                {
                    Destroy(this.gameObject.GetComponent<Collider>());
                        
                            
                }

                gameObject.SetActive(true);
                       
                propSize = hit.collider.tag;
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
                break;
                    
            case "Medium Item":
                currentHP = currentHP * 100 / maxHP;
                maxHP = 100;
                break;
            case "Big Item":
                currentHP = currentHP * 200 / maxHP;
                maxHP = 200;
                break;
        }
    }
              
}