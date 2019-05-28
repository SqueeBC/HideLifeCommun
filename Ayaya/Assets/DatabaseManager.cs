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
    
    void Awake()
    {
        string constr = "Server=" + host + ";DATABASE=" + database + ";User ID=" + username + ";Password=" + password +
                        ";Pooling=true;Charset=utf8;";
        
        try
        {
            con = new MySqlConnection(constr);
            con.Open();
            TxtState.text = con.State.ToString();
        }
        catch (IOException Ex)
        {
            TxtState.text = Ex.ToString();
        }
    }
    
    void Update()
    {
        
    }
}
