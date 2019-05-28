using System.Collections.Generic;
using System.Linq;
using trucs_perso;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class PlayerMotor : MonoBehaviour
{        
    [SerializeField] //permet de modifier la valeur via Unity
    private Camera camera;
    private Vector3 velocity; //le déplacement du joueur
    private Vector3 rotation; //la rotation de la camera
    private float cameraRotationX; 
    private float currentCameraRotationX = 0f;
    private Rigidbody rb;
    private int Jump = 12; //à modifier
    private ForceMode JumpForce; 
    bool isGrounded = false;
    public Player player;    
    private float Gravitydmg = -15;
    [SerializeField] private float cameraRotationLimit = 85f; //Permet d'empecher le bug de cam 
    float yRot;
    float xRot;
    private bool IsMoving;
    float lookSensitivity = 3f;
    private float notmovingtime = 20;
    private List<AudioSource> taunts;
   

    private void Start()
    {
        notmovingtime = 20;
        
        taunts = GameObject.Find("Taunts").GetComponentsInChildren<AudioSource>().ToList();
        
        player = GetComponentInParent<Player>();
        rb = GetComponent<Rigidbody>(); //on implémente le rigidbody au début
    }

    public void Move(Vector3 velocity)
    {
        this.velocity = velocity; //on modifie le déplacement
    }

    void OnCollisionEnter(Collision collision) //est appelée quand le joueur touche le sol
    {
       
        if (collision.gameObject.CompareTag("Ground")||collision.gameObject.CompareTag("Item"))
        {     
            isGrounded = true;
        }
    }

    private void Update()
    {
      
        if(notmovingtime>0&&!IsMoving)
        notmovingtime -= Time.deltaTime;
        if (Input.GetKeyUp((KeyCode) System.Enum.Parse(typeof(KeyCode),PlayerPrefs.GetString("JumpKey", "Space"))) && isGrounded) // si le joueur n'est pas sur le sol, il ne peut pas sauter.
        {    
            Debug.Log(PlayerPrefs.GetString("JumpKey"));
            PlayerJump();
            isGrounded = false;
        }

        if (notmovingtime<=0&&taunts.Count>0)
        {
            System.Random randomtaunt = new System.Random();
            AudioSource.PlayClipAtPoint(taunts[randomtaunt.Next(taunts.Count)].clip,this.transform.position);
            notmovingtime = 20;
        }
      
    }

    private void FixedUpdate() //recommandé pour le rigidBody
    {
       
        if(!isGrounded)
            Gravitydmg += (Time.deltaTime)*30;
        else
        {

            if (Gravitydmg > 0)
                player.TakeDamage( Mathf.RoundToInt(Gravitydmg));
            Gravitydmg = -50; //reset des dmg
        }
        PerformMovement();
        PerformRotation();
    }
    
    private void PerformMovement()
    {
        if (velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position+velocity * Time.fixedDeltaTime);
            
            notmovingtime = 20;
        }
        IsMoving = velocity != Vector3.zero;
    //Bouge le personnage en fonction du temps 
        
    }
    public void Rotate (Vector3 rotation) //on modifie la valeur de rotation de la caméra
    {
        this.rotation = rotation;
    }
    public void RotateCamera (float camerarotationX)
    {
        cameraRotationX = camerarotationX;
    }

    private void PerformRotation()
    {
        
        //récuperation de la rotation
            rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation)); //fonction d'Unity qui fait une rotation au rigidbody
            //les Quaternions représentent des rotations.
            currentCameraRotationX -= cameraRotationX; //mettre un + pour caméra invers&e
            currentCameraRotationX =
                Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit,
                    cameraRotationLimit); //pour bloquer la caméra entre une valeur max et une valeur min 
            //application des changement apres le clamp
            camera.transform.localEulerAngles =new Vector3(currentCameraRotationX,0f,0f);




    }

    private void PlayerJump()
    {
        JumpForce = ForceMode.Impulse;
        rb.AddForce(0,Jump,0,JumpForce); //le type de force
        
    }
    
 
    
    
}
