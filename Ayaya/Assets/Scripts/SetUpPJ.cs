using UnityEngine;
using UnityEngine.Networking;

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
        if (gameObject.transform.GetComponent<Hunter>() != null)
        {
            gameObject.transform.GetChild(1).GetComponent<PlayerMotor>().enabled = true;
            gameObject.transform.GetChild(1).GetComponent<PlayerControler>().enabled = true;
            gameObject.transform.GetChild(1).GetChild(0).GetComponent<Camera>().enabled = true;
            gameObject.transform.GetChild(1).GetChild(0).GetComponent<AudioListener>().enabled = true;
            Debug.Log("coucou t'es un hunter et tu existe");
        }
        else
        {
            gameObject.transform.GetChild(0).GetComponent<PlayerMotor>().enabled = true;
            gameObject.transform.GetChild(0).GetComponent<PlayerControler>().enabled = true;
            gameObject.transform.GetChild(0).GetChild(0).GetComponent<Camera>().enabled = true;
            gameObject.transform.GetChild(0).GetChild(0).GetComponent<AudioListener>().enabled = true;
            Debug.Log("coucou t'es un prop et tu existe");
        }
    }

    private void OnDisable()
    {
        if (cam != null)
            cam.gameObject.SetActive(true);
    }
}