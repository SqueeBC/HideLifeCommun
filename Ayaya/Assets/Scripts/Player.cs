using System;
using System.Collections;
using UnityEditor.Experimental.UIElements;
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

        public string id;
        
        public int victory;
        //mettre pour le multi[SyncVar] //syncronise avec le serveur
        public int currentHP;
        public float _time = 2;
        

        private void Start()
        {
            StopCoroutine(waiter());
            id = GetComponent<NetworkIdentity>().netId.ToString();
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        }

        IEnumerator waiter()
        {
            yield return new WaitForSeconds(4);
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
                if (SceneManager.GetActiveScene().buildIndex != 6||!GetComponent< NetworkIdentity>().isLocalPlayer)
                {
                    gameManager.UnRegisterPlayer("Player " + id);
                    Spectate();

                }
                else
                {
                    currentHP = maxHP;
                    gameObject.transform.position = new Vector3(-152.33f, 15.6286f,-64);
                }
            }
            

        }

        public void Spectate()
        {
            if(gameObject.GetComponent<PlayerShoot>()!=null)
                Destroy(gameObject.GetComponent<PlayerShoot>());
            Destroy(gameObject.transform.GetChild(1).gameObject);
            gameObject.GetComponent<Rigidbody>().useGravity = false;
            gameObject.tag = "Spectator"; 
        }

        private void Update() 
        {

            if (_time > -1 && gameObject.GetComponent<Collider>() != null)
            { 
                if (CompareTag("Spectator"))
                {
                    
                    _time -= Time.deltaTime;

                }

                if (_time <= 0)
                    Destroy(gameObject.GetComponent<Collider>());
         

            }
        }
    }
}