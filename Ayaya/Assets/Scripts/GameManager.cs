using System.Collections.Generic;
using trucs_perso;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private Text WinText;
        private float time = 600;
        private static Dictionary<string, Player> players = new Dictionary<string, Player>();

        public void RegisterPlayer(string netID , Player player)//l'id du joueur selon le serv     
        {
            string playerID = "Player " + netID;
            players.Add(playerID, player);          
        }

        public void VictoryForProps()
        {
            if (PlayerPrefs.GetString("language", "english") == "english")
                WinText.text = "Props have won !";
            else
            {
                WinText.text = "Les Props ont gagné !";
            }
          
                foreach (Prop prop in players.Values)
                {
                    prop.victory++;
                }
        }
        public void VictoryForHunters()
        {
            if (PlayerPrefs.GetString("language", "english") == "english")
                WinText.text = "Hunters have won !";
            else
            {
                WinText.text = "Les Chasseurs ont gagné !";
            }
          
            foreach (Hunter hunter in players.Values)
            {
                hunter.victory++;
            }
        }

        public void UnRegisterPlayer(string  playerID)
        {
            players.Remove(playerID);
        }

        public static Player GetPlayer(string playerID)
        {
            return players[playerID];
        }

        private void OnGUI() //permet d'afficher les joueurs
        {
            int Min;
            GUILayout.BeginArea(new Rect(200, 200, 200, 200));
                GUILayout.BeginVertical();
                foreach (string playerID in players.Keys)
                {
                       GUILayout.Label(playerID + "-"+players[playerID].transform.name);
                }

                Min = Mathf.RoundToInt((time / 60 - 0.5f));
                GUILayout.Label("-Time :" + Mathf.RoundToInt((Min )) +  "min "+Mathf.RoundToInt(time-Min*60)+" s");
              GUILayout.EndVertical();
                GUILayout.EndArea();
        }

        private void Update()
        {
            foreach (var player in FindObjectsOfType<Player>())
            {
               
                if (!players.ContainsKey("Player " + player.id) && player != null)
                {Debug.Log(player.name);
                    RegisterPlayer(player.id, player);
                    if(player.gameObject.GetComponent<Hunter>()==null&&player.gameObject.GetComponent<Prop>()==null)
                    
                    AssignRole(player);}
                
                
            }
            if(time<=0)
                VictoryForProps();
            bool Mybool = true;
            foreach (Player player in players.Values)
            {
                Mybool = Mybool && player.gameObject.GetComponent<Prop>() == null;
            }
                if(Mybool)
                    VictoryForHunters();
            time -= Time.deltaTime;

        }

        private void AssignRole(Player player)
        {

            UnRegisterPlayer("Player "+player.id);
            Destroy(player);    
            
            
            int nbrhuntertot = 0;
           int nbrhunter=  Mathf.RoundToInt(players.Count * 3 / 10+1);
           foreach (Player _player in players.Values)
           {
               if (_player.GetComponent<Hunter>() != null)
                   nbrhuntertot++;                  
           }
            Debug.Log(nbrhunter);
           if (nbrhuntertot < nbrhunter)
           {
               Debug.Log(player.gameObject.name);
               player.gameObject.AddComponent<Hunter>();
               player.gameObject.GetComponent<Hunter>().id = player.id;
              
           }
           else
           {
               player.gameObject.AddComponent<Prop>();
               player.gameObject.GetComponent<Prop>().id = player.id;
              

           }
         
            
        }
    }
    
