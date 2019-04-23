using UnityEngine; 
using UnityEngine.Networking; 
 
public class NetworkManager : NetworkBehaviour
{ 
    public Behaviour[] compoToDisable; 
    Camera cam; 
    private void Start() 
    { 
        if(!isLocalPlayer) 
        { 
            for(int i = 0; i<compoToDisable.Length; i++) 
            { 
                compoToDisable[i].enabled = false; 
            } 
        } 
        else 
        { 
            cam = Camera.main; 
            if(cam != null) 
                cam.gameObject.SetActive(false);
        } 
    } 
 
    private void OnDisable() 
    { 
        if (cam != null)
            cam.gameObject.SetActive(true);
    } 
}
