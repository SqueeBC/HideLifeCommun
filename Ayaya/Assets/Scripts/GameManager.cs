using System.Collections.Generic;
using trucs_perso;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : NetworkManager 
{
    [SerializeField] private Text WinText;
    private float time = 600;
    private static Dictionary<string, Player> players = new Dictionary<string, Player>();
    private bool start;

    public void RegisterPlayer(string netID, Player player) //l'id du joueur selon le serv     
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

    public void UnRegisterPlayer(string playerID)
    {
        players.Remove(playerID);
    }

    public static Player GetPlayer(string playerID)
    {
        return players[playerID];
    }
    

    private void OnGUI() //permet d'afficher les joueurs
    {
        string spect = "dead";
        GUIStyle style = new GUIStyle(); //pour changer la police du texte
        int Min;
        GUILayout.BeginArea(new Rect(200, 200, 200, 200));
        GUILayout.BeginVertical();
        if(!start)
        start = GUILayout.Button("Ready");
        foreach (string playerID in players.Keys)
        {
            GUILayout.Label(playerID + "-" + players[playerID].transform.name, style);
        }

        style.fontSize = 24;
        Min = Mathf.RoundToInt((time / 60 - 0.5f));
        GUILayout.Label("-Time :" + Mathf.RoundToInt((Min)) + "min " + Mathf.RoundToInt(time - Min * 60) + " s", style);
        GUILayout.EndVertical();
        GUILayout.EndArea();

        style.fontSize = 14;
        GUILayout.BeginArea(new Rect(950, 200, 200, 200));
        GUILayout.BeginVertical();
        GUILayout.Label("Prop :", style);
        style.fontSize = 20;
        foreach (Player prop in players.Values)
        {
            if (prop.GetComponent<Prop>() != null)
            {
                if (prop.CompareTag("Spectator"))
                {

                    GUILayout.Label("-DEAD " + prop.transform.name, style);
                }
                else
                {
                    GUILayout.Label("-" + prop.transform.name, style);
                }
            }
        }

        GUILayout.EndVertical();
        GUILayout.EndArea();
        GUILayout.BeginArea(new Rect(750, 200, 200, 200));
        GUILayout.BeginVertical();
        style.fontSize = 14;
        GUILayout.Label("Hunter :", style);
        style.fontSize = 20;
        foreach (Player hunter in players.Values)
        {
            if (hunter.GetComponent<Hunter>() != null)
            {
                if (hunter.CompareTag("Spectator"))
                    GUILayout.Label("-DEAD " + hunter.transform.name, style);
                else
                {
                    GUILayout.Label("-" + hunter.transform.name, style);
                }
            }
        }

        GUILayout.EndVertical();
        GUILayout.EndArea();

    }
    [Server]
    private void Update()
    { 
        
        
        if(start)
        foreach (var player in FindObjectsOfType<Player>())
        {

            if (!players.ContainsKey("Player " + player.id) && player != null)
            {

                RegisterPlayer(player.id, player);
            }

            if (player.gameObject.GetComponent<Hunter>() == null && player.gameObject.GetComponent<Prop>() == null)
            {
                if (SceneManager.GetActiveScene().buildIndex != 6)
                {
                   
                        AssignRole(player);


                }

                else
                {
                    if (player.gameObject.GetComponent<NetworkIdentity>().isLocalPlayer) //pour le tuto
                    {


                        UnRegisterPlayer("Player " + player.id);
                        Destroy(player);
                        player.gameObject.AddComponent<Hunter>();
                        player.gameObject.GetComponent<Hunter>().id = player.id;
                    }

                }


            }
        }
        if (SceneManager.GetActiveScene().buildIndex != 6&&start)
        {
            if (time <= 0)
                VictoryForProps();
            bool Mybool = true;
            foreach (Player player in players.Values)
            {
                Mybool = Mybool && (player.gameObject.GetComponent<Prop>() == null || player.gameObject.CompareTag("Spectator"));
            }

            if (Mybool)
                VictoryForHunters();
            time -= Time.deltaTime;

        }

        Debug.Log(start);


    }

    private void AssignRole(Player player)
    {
        
            
        int nbrhuntertot = 0;
        int nbrhunter=  Mathf.RoundToInt(players.Count * 3 / 10+1);
        foreach (Player _player in players.Values)
        {
            if (_player.GetComponent<Hunter>() != null)
                nbrhuntertot++;                  
        }
          
        if (nbrhuntertot < nbrhunter)
        {
               
            player.gameObject.AddComponent<Hunter>();
            player.gameObject.GetComponent<Hunter>().id = player.id;
              
        }
        else
        {
            player.gameObject.AddComponent<Prop>();
            player.gameObject.GetComponent<Prop>().id = player.id;
              

        }
         
            
        UnRegisterPlayer("Player "+player.id);
        Destroy(player);    

    }
}