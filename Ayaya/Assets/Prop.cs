using System.Collections;
using System.Collections.Generic;
using trucs_perso;
using UnityEngine;

   public class Prop : Player
        {  
            private Camera camera;
            
            private string propSize;
            private void Start()
            {    
           
                gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
                maxHP = 100;
                currentHP = maxHP;
            }

            public void TransformationTest() //fonction provisoire pour les tests
            {
                RaycastHit hit;
                if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, 1000)) ;
                {
                    Debug.Log(hit.collider.name);
                    if (hit.collider != null)
                    {
                       
                        Destroy(transform.FindChild("Graphics").gameObject);
                        GameObject gameObject = Instantiate(hit.collider.transform.gameObject);
                        gameObject.name = "Graphics";
                        gameObject.transform.localPosition = transform.localPosition;
                        gameObject.transform.parent = transform;


                        int i = 0;
                        Debug.Log(gameObject.transform.localPosition.x);
                        while (i < gameObject.transform.childCount)
                        {
                            GameObject newChild = new GameObject();
                            newChild = gameObject.transform.GetChild(i).gameObject;
                            if (gameObject.transform.GetChild(i).gameObject.transform.childCount > 0)
                            {
                                int j = 0;
                                while (j < gameObject.transform.GetChild(i).gameObject.transform.childCount)
                                {
                                    GameObject newChildForThisChild = new GameObject();
                                    newChildForThisChild.transform.parent =
                                        gameObject.transform.GetChild(i).gameObject.transform;
                                    newChildForThisChild.transform.localPosition =
                                        gameObject.transform.GetChild(i).gameObject.transform.position;
                                    j++;
                                }
                            }

                            newChild.transform.parent = gameObject.transform;
                            newChild.transform.localPosition = gameObject.transform.position;

                            i++;
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