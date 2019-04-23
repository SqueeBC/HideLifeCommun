using System;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.Utility;

namespace trucs_perso
{       
    public class Player : MonoBehaviour //a remplacer avec NetworkBehaviour pour le multi
    
    //EN CONSTRUCTION
    {
      
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
                gameManager.UnRegisterPlayer("Player "+id);
            }
            
        }

        private void Update()
        {
            Death();
        }
    }
}

   