using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using trucs_perso;
using UnityEngine;
using UnityEngine.Networking;

//Le système de tir

public class PlayerShoot : MonoBehaviour //NETWORKBEHAVIOUR A REMPLACER
{
    public PlayerWeapon weapon;
        [SerializeField]
    private Camera cam;

    private float AudioTimer = 0.5f; //pour que l'audio s'arrête au bout d'un certain temps
    public float ReloadTime;
    public AudioSource shotaudio;
    private Player Target;
    public  GameManager gameManager;


    [SerializeField] private LayerMask mask; //permet de ne pas ce toucher soi-même lors du tir

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (cam == null)
        {
            Debug.LogError("Pas de caméra détectée.");
            this.enabled = false;
        }
    }


  private void Update()


  {
      if(AudioTimer>0)
          AudioTimer-=Time.deltaTime;
      
      if(AudioTimer<=0)
          shotaudio.Pause();
     
      if (weapon.WeaponCooldown > 0)
          weapon.WeaponActualCooldown -= Time.deltaTime;
      
      if(ReloadTime>0)
      ReloadTime -= Time.deltaTime;
     
      
        if(weapon.ammo==0&&Input.GetButton("Fire1")||Input.GetKey( (KeyCode) System.Enum.Parse(typeof(KeyCode),PlayerPrefs.GetString("ReloadKey", "R"))))
            Reload();
            
        
       if(Input.GetButton("Fire1")&&weapon.ammo>0&&ReloadTime<=0&&weapon.WeaponActualCooldown<=0f) //GetButtonDown signifie que le bouton à été appuyé au moins une fois, contrairement au GetButton qui signifie que le bouton est maintenu.
       {    
           weapon.WeaponActualCooldown = weapon.WeaponCooldown;
           Shoot();
        }

    }

  private void Shoot()
  {
      
      
      if (!shotaudio.isPlaying)
      {
          shotaudio.Play();
          AudioTimer = 0.5f;
      }
     
      

      weapon.ammo -= 1;    
      RaycastHit hit; //Raycast = litteralement lanceur de rayon, lance un rayon d'une certaine distance et direction s'arrêtant devant le 1er obstacle touché.
      if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, weapon.range, mask)) ;
      {
          
          if (hit.collider != null)         
              Debug.Log("Objet touché" + hit.collider.name);
          if (hit.collider.CompareTag("Player"))
          { 
              GetTarget(hit.collider.GetComponent<Player>().id,weapon.dmg);
          }


      }
      
  }

  public static void GetTarget(string id, int dmg)
  {

     Player Target = GameManager.GetPlayer("Player "+id);
     Debug.Log(Target.currentHP);
     Target.TakeDamage(dmg);
  }
  

  

  private void Reload()
  {
      if (weapon.totalammo != 0)
      {
          
          ReloadTime = 2f;
          int possibleammo = weapon.chargercapacity - weapon.ammo % weapon.totalammo;
      weapon.totalammo -= possibleammo;
      weapon.ammo +=possibleammo;
  }
}
}
