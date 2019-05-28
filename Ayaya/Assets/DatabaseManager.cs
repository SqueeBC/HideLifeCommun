using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;
using MySql.Data.MySqlClient;
using Debug = System.Diagnostics.Debug;

public class DatabaseManager : MonoBehaviour
{
    public string host;
    public string database;
    public string username;
    public string password;
    public Text TxtState;
    MySqlConnection con;
    public InputField IfLogin;
    public InputField IfPassword;
    public Text TxtLogin;
    
    void ConnectBDD()
    {
        string constr = "Server=" + host + ";DATABASE=" + database + ";User ID=" + username + ";Password=" + password +
                        ";Pooling=true;Charset=utf8;";
        
        try
        {
            con = new MySqlConnection(constr);
            con.Open();
            TxtState.text = "[Base De Donnée] :  " + con.State.ToString();
        }
        catch (IOException Ex)
        {
            TxtState.text = Ex.ToString();
        }
    }

    void OnApplicationQuit()
    {
        TxtState.text = ("[Base De Donnée] :  Fermeture de la connexion");
        
        Debug.Write("Fermeture Connection Base de Données");

        if (con != null && con.State.ToString() != "Closed")
        {
            con.Close();
        }
    }

    public void Register()
    {
        ConnectBDD();

        bool Exist = false;
        
        //Vérification pseudo existant
        MySqlCommand commandsql = new MySqlCommand("SELECT pseudo FROM users WHERE pseudo ='" + IfLogin.text + "'", con);
        MySqlDataReader MyReader = commandsql.ExecuteReader();

        while (MyReader.Read())
        {
            if (MyReader["pseudo"].ToString() != "")
            {
                TxtLogin.text = "Pseudo Already Exist !";
                Exist = true;
            }
        }
        MyReader.Close();

        if (!Exist)
        {
            string command = "INSERT INTO users VALUES (defauft,'" + IfLogin.text + "','" + IfPassword + "','')";
            MySqlCommand cmd = new MySqlCommand(command, con);

            try
            {
                cmd.ExecuteReader();
                TxtLogin.text = "Register Successful";
            }
            catch (IOException Ex)
            {
                TxtState.text = Ex.ToString();
            }
        
            cmd.Dispose();
            con.Close();
        }
    }
}
