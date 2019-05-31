using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Mime;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using MySql.Data.MySqlClient;
using UnityEngine.SceneManagement;
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
    public Text BtnLoginText;
    public Text BtnRegisterText;
    public Text TxtMessageCo;
    public Dropdown _dropdown;
    public Text placeholder;
    struct _Player
    {
        public int ID;
        public string Pseudo;
        public string Password;
        public string Email;
        public int Wins;
    }
    _Player Player;

    private void Update()
    {
        Changelanguage(_dropdown);
        if (PlayerPrefs.GetString("language", "english") == "français")
        {
            BtnRegisterText.text = "s'enregistrer";
            BtnLoginText.text = "se connecter";
            TxtMessageCo.text = "Connection";
            placeholder.text = "mot de passe";
        }
        else
        {
            BtnRegisterText.text = "register";
            BtnLoginText.text = "login";
            TxtMessageCo.text = "Connexion";
            placeholder.text = "passeword";
        }
    }

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
        
        /*SHA1 chiffrage = new SHA1CryptoServiceProvider();
        byte[] chiffragemdp = chiffrage.ComputeHash(buffer: System.Text.Encoding.ASCII.GetBytes(IfPassword.text + "1254"));
        string mdpchiffrefin = System.Text.Encoding.UTF8.GetString(chiffragemdp, 0, chiffragemdp.Length);
        IfPassword.text = "aq1" + mdpchiffrefin + "25";*/
        
        SHA1CryptoServiceProvider sh = new SHA1CryptoServiceProvider();
        sh.ComputeHash(ASCIIEncoding.ASCII.GetBytes(IfPassword.text + "1254"));
        byte[] re = sh.Hash;
        StringBuilder sb = new StringBuilder();
        foreach (var b in re)
        {
            sb.Append(b.ToString("x2"));
        }
        IfPassword.text = "aq1" + sb.ToString() + "25";
        
        if (IfLogin.text == "")
        {
            if(PlayerPrefs.GetString("language","english")=="français")
                TxtLogin.text = "Cases Vides !";
            else
            {
                TxtLogin.text = "Empty boxes!"; // a vérifier, pas sûr de la traduction
            }
        }
        else
        {
            try
            {
                MySqlCommand commandesql = new MySqlCommand("SELECT * FROM users WHERE email ='" + IfLogin.text + "'", con);
                MySqlDataReader Myreader = commandesql.ExecuteReader();

                while (Myreader.Read())
                {
                    pass = Myreader["password"].ToString();

                    if ((pass == IfPassword.text) && (IfLogin.text != ""))
                    {
                        Player.ID = (int) Myreader["ID"];
                        Player.Pseudo = Myreader["pseudo"].ToString();
                        Player.Password = Myreader["password"].ToString();
                        Player.Email = Myreader["email"].ToString();
                        Player.Wins = (int)Myreader["wins"];
                        if(PlayerPrefs.GetString("language","english")=="français")
                            TxtLogin.text = "Bienvenue, " + Player.Pseudo + "! Vous avez gagné " + Player.Wins + " parties !"; 
                        else
                        {


                            TxtLogin.text = "Welcome, " + Player.Pseudo + "! You won " + Player.Wins + " games !";

                        }

                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

                    }
                    else
                    { if(PlayerPrefs.GetString("language","english")=="français")
                            TxtLogin.text = "Pseudo/mot de passe invalide";
                        else
                        {
                            TxtLogin.text = "Invalid pseudo/password";
                        }
                    }
                }

                if (pass == null)
                {
                    if (PlayerPrefs.GetString("language", "english") == "français")
                        TxtLogin.text = "Ce compte n'existe pas !";
                    else
                    {


                        TxtLogin.text = "No existing account !";
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
    public void Changelanguage(Dropdown dropdown)
    { 
           
        if (dropdown.value==0)
            PlayerPrefs.SetString("language", "english");

        else if (dropdown.value == 1)    
            PlayerPrefs.SetString("language", "français");
        
    }
}