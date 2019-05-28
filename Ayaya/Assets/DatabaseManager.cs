using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Mime;
using System.Security.Cryptography.X509Certificates;
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
        
        Process p = new Process();
        p.StartInfo.FileName = "http://hidelife.fr";
        p.Start();
        
        ConnectBDD();


        bool Exist = true;
        /*bool Exist = false;
        
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
        MyReader.Close();*/
        con.Close();

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

    public void Login()
    {
        ConnectBDD();
        string pass = null;
        
        if (IfLogin.text == "")
        {
            TxtLogin.text = "Cases Vides !";
        }
        else
        {
            try
            {
                MySqlCommand commandesql = new MySqlCommand("SELECT * FROM users WHERE pseudo ='" + IfLogin.text + "'", con);
                MySqlDataReader Myreader = commandesql.ExecuteReader();

                while (Myreader.Read())
                {
                    pass = Myreader["password"].ToString();

                    if ((pass == IfPassword.text) && (IfLogin.text != ""))
                    {
                        TxtLogin.text = "Welcome !";
                    }
                    else
                    {
                        TxtLogin.text = "Invalid pseudo/password";
                    }
                }
                Myreader.Close();
            }
            catch (IOException Ex)
            {
                TxtState.text = Ex.ToString();
            }
        }
        con.Close();
    }
}
