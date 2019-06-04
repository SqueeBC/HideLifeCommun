using UnityEngine;
using UnityEngine.Networking;

//gère les cam et mouvement en réseau

public class SetUpPJ : NetworkBehaviour
{
    public Behaviour[] compoToDisable;
    Camera cam;

    //inutile donc indispansable (permet à la classe d'exister mais fait rien...)
    public SetUpPJ() { }

    private void Start()
    {
        if (!isLocalPlayer)
        {
            for (int i = 0; i < compoToDisable.Length; i++)
            {Debug.Log(gameObject.name);
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

    private void OnDisable()
    {
        if (cam != null)
            cam.gameObject.SetActive(true);
    }
}