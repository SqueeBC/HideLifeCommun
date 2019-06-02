using UnityEngine;
using UnityEngine.Networking;
using Random = System.Random;

//gère les cam et mouvement en réseau

public class SetUpPJ : NetworkBehaviour
{
    public Behaviour[] compoToDisable;
    public bool manualSpawn = false;
    Camera cam;

    //inutile donc indispansable (permet à la classe d'exister mais fait rien...)
    public SetUpPJ(){}

    public void Start()
    {
        
        if (manualSpawn)
        {
            Spawn();
        }
        else
        {
            if (!isLocalPlayer)
            {
                for (int i = 0; i < compoToDisable.Length; i++)
                {
                    compoToDisable[i].enabled = false;
                }
            }
            else
            {
                cam = Camera.main;
                if (cam != null)
                    cam.gameObject.SetActive(false);
            }
        }
    }

    public void Spawn()
    {
        Random rng = new Random();
        //if (rng.Next(1)==1)
        if (gameObject.transform.GetComponent<Hunter>())
        {
            /*if (!gameObject.transform.GetComponent<Hunter>())
                gameObject.AddComponent<Hunter>();
            else
                gameObject.transform.GetComponent<Hunter>().enabled = true;
            if (gameObject.transform.GetComponent<Prop>())
                gameObject.transform.GetComponent<Prop>().enabled = false;*/
            gameObject.transform.GetChild(1).GetComponent<PlayerMotor>().enabled = true;
            gameObject.transform.GetChild(1).GetComponent<PlayerControler>().enabled = true;
            gameObject.transform.GetChild(1).GetChild(0).GetComponent<Camera>().enabled = true;
            gameObject.transform.GetChild(1).GetChild(0).GetComponent<AudioListener>().enabled = true;/*
            gameObject.transform.GetChild(0).GetComponent<PlayerMotor>().enabled = false;
            gameObject.transform.GetChild(0).GetComponent<PlayerControler>().enabled = false;
            gameObject.transform.GetChild(0).GetChild(0).GetComponent<Camera>().enabled = false;
            gameObject.transform.GetChild(0).GetChild(0).GetComponent<AudioListener>().enabled = false;*/
        }
        else
        {
            /*if (gameObject.transform.GetComponent<Hunter>())
                gameObject.transform.GetComponent<Hunter>().enabled = false;
            if (!gameObject.transform.GetComponent<Prop>())
                gameObject.AddComponent<Prop>();
            else
                gameObject.transform.GetComponent<Prop>().enabled = true;
            gameObject.transform.GetChild(1).GetComponent<PlayerMotor>().enabled = false;
            gameObject.transform.GetChild(1).GetComponent<PlayerControler>().enabled = false;
            gameObject.transform.GetChild(1).GetChild(0).GetComponent<Camera>().enabled = false;
            gameObject.transform.GetChild(1).GetChild(0).GetComponent<AudioListener>().enabled = false;*/
            gameObject.transform.GetChild(0).GetComponent<PlayerMotor>().enabled = true;
            gameObject.transform.GetChild(0).GetComponent<PlayerControler>().enabled = true;
            gameObject.transform.GetChild(0).GetChild(0).GetComponent<Camera>().enabled = true;
            gameObject.transform.GetChild(0).GetChild(0).GetComponent<AudioListener>().enabled = true;
        }
    }

    private void OnDisable()
    {
        if (cam != null)
            cam.gameObject.SetActive(true);
    }
}