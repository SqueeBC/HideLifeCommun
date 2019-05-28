using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MySql.Data.MySqlClient;

public class DatabaseManager : MonoBehaviour
{
    public string host;
    public string database;
    public string username;
    public string password;
    public Text TxtState;
    MySqlConnection con;
    
    void Start()
    {
        string constr = "Server=" + host + ";DATABASE=" + database + ";User ID=" + username + ";Password=" + password +
                        ";Pooling=true;Charset=utf8;";
        
        try
        {
            con = new MySqlConnection(constr);
            con.Open();
        }
        catch (IOException Ex)
        {
            TxtState.text = Ex.ToString();
        }
    }
    
    void Update()
    {
        TxtState.text = "[Base De Donnée] :  " + con.State.ToString();
    }

    void OnApplicationQuit()
    {
        TxtState.text = ("[Base De Donnée] :  Fermeture de la connexion");

        if (con != null && con.State.ToString() != "Closed")
        {
            con.Close();
        }
    }
}
