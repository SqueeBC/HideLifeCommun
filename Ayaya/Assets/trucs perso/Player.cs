using System;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Utility;

namespace trucs_perso
{
    public class Player : NetworkBehaviour //a remplacer avec NetworkBehaviour pour le multi

        //EN CONSTRUCTION
    {   [SerializeField]             
        public GameManager gameManager;

        [SerializeField] public int maxHP;

        [SerializeField] public string id;
        
        public int victory;
        //mettre pour le multi[SyncVar] //syncronise avec le serveur
        public int currentHP;

        

        private void Start()
        {
           

            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        }


        public void TakeDamage(int dmg)
        {
            currentHP -= dmg;
                    
            Death();
        }

        public void Death()
        {
            if (currentHP <= 0)
            {
                if (SceneManager.GetActiveScene().buildIndex != 6)
                {
                    Destroy(gameObject);
                    gameManager.UnRegisterPlayer("Player " + id);
                }
                else
                {
                    currentHP = maxHP;
                    gameObject.transform.position = new Vector3(-152.33f, 15.6286f,-64);
                }
            }
            

        }

    


       
     
    }
}