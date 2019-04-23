using System.Collections.Generic;
using trucs_perso;
using UnityEngine;


    public class GameManager : MonoBehaviour
    {
        private static Dictionary<string, Player> players = new Dictionary<string, Player>();

        public void RegisterPlayer(string netID , Player player)//l'id du joueur selon le serv     
        {
            string playerID = "Player " + netID;
            players.Add(playerID, player);
          

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
            GUILayout.BeginArea(new Rect(200, 200, 200, 200));
                GUILayout.BeginVertical();
                foreach (string playerID in players.Keys)
                {
                       GUILayout.Label(playerID + "-"+players[playerID].transform.name);
                }
                GUILayout.EndVertical();
                GUILayout.EndArea();
        }

        private void Update()
        {
            foreach (var player in FindObjectsOfType<Player>())
            {

                if (!players.ContainsKey("Player "+player.id) && player != null)
                 RegisterPlayer(player.id, player);
                }
        
            }
     
        
        }
    
