using System;
using UnityEngine;
using UnityEngine.Networking;
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
            Debug.Log(transform.name + "has" + currentHP + "HP");
            Death();
        }

        public void Death()
        {
            if (currentHP <= 0)
            {
                Destroy(gameObject);
                gameManager.UnRegisterPlayer("Player " + id);
            }
            

        }

    


       
     
    }
}