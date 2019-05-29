using System;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.Utility;

namespace trucs_perso
{
    public class Player : NetworkBehaviour //a remplacer avec NetworkBehaviour pour le multi

        //EN CONSTRUCTION
    {   [SerializeField]
        private Camera camera;
        private string propSize;
        public GameManager gameManager;
        [SerializeField] public int maxHP;

        [SerializeField] public string id;

        //mettre pour le multi[SyncVar] //syncronise avec le serveur
        public int currentHP;

        protected bool HasWeapons;

        private void Start()
        {    
           
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        }


        public void TakeDamage(int dmg)
        {
            currentHP -= dmg;
            Debug.Log(transform.name + "has" + currentHP + "HP");
        }

        public void Death()
        {
            if (currentHP <= 0)
            {
                Destroy(gameObject);
                gameManager.UnRegisterPlayer("Player " + id);
            }

        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
                TransformationTest();
            Death();
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
    }
}