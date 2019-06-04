using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Utility;

namespace trucs_perso
{
    public class Player : NetworkBehaviour 

        //EN CONSTRUCTION
    {   [SerializeField]             
        public GameManager gameManager;

        [SerializeField] public int maxHP;

        public string id;
        [SyncVar]
        public int victory;
        //mettre pour le multi[SyncVar] //syncronise avec le serveur
        [SyncVar]
        public int currentHP;
        public float _time = 2;
        

        private void Start()
        {
            if (isLocalPlayer)
                gameObject.layer = 9;
            maxHP = 100;
            currentHP = 100;
            id = GetComponent<NetworkIdentity>().netId.ToString();
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
                if (SceneManager.GetActiveScene().buildIndex != 6||!GetComponent< NetworkIdentity>().isLocalPlayer)
                {
                    gameManager.UnRegisterPlayer("Player " + id);
                    if(!CompareTag("Target"))
                    Spectate();
                    else
                    {
                        Destroy(gameObject);
                    }

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
            Destroy(gameObject.transform.GetChild(0).gameObject);
            gameObject.GetComponent<Rigidbody>().useGravity = false;
            gameObject.tag = "Spectator"; 
            GameObject camera = new GameObject();
            camera.AddComponent<Camera>();
            camera.transform.parent = gameObject.transform;
            camera.transform.rotation = new Quaternion(0,0,0,0);
            camera.transform.position = transform.position;
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