using System.Collections.Generic;
using System.Linq;
using trucs_perso;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(Rigidbody))]

public class PlayerMotor : MonoBehaviour
{        
    [SerializeField] //permet de modifier la valeur via Unity
    private Camera camera;
    private Vector3 velocity; //le déplacement du joueur
    private Vector3 rotation; //la rotation de la camera
    private float cameraRotationX; 
    private float currentCameraRotationX = 0f;
    public Rigidbody rb;
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
    public bool hasJumped = false;
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
        if (player == null)
            player = GetComponentInParent<Player>();
        if (!gameObject.CompareTag("Spectator"))
        {
            if (notmovingtime > 0 && !IsMoving)
                notmovingtime -= Time.deltaTime;
            if (Input.GetKeyUp((KeyCode) System.Enum.Parse(typeof(KeyCode),   PlayerPrefs.GetString("JumpKey", "Space"))) &&((rb.velocity.y <= 0.65f && rb.velocity.y >= -0.1))) // si le joueur n'est pas sur le sol, il ne peut pas sauter.
            {

                PlayerJump();
                isGrounded = false;
            }

            if (notmovingtime <= 0 && taunts.Count > 0)
            {
                
                System.Random randomtaunt = new System.Random();
                AudioSource.PlayClipAtPoint(taunts[randomtaunt.Next(taunts.Count)].clip, this.transform.position,1+PlayerPrefs.GetFloat("Volume")/ 80);
                notmovingtime = 20;
            }

        }
        else
        {
            if (Input.GetKey((KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("JumpKey", "Space"))))
                PlayerJump();
            rb.velocity = new Vector3(0,0,0); 
            if(Input.GetKey(KeyCode.LeftControl))
                SpectatorFall();
            rb.velocity = new Vector3(0,0,0); 
                
        }
    }

    private void FixedUpdate() //recommandé pour le rigidBody
    {
        if(!gameObject.CompareTag("Spectator"))
        { int test = Mathf.RoundToInt(rb.velocity.y);

        if (test < 0)
            Gravitydmg += -test ;
         
            if (Gravitydmg > 0)
                player.TakeDamage( Mathf.RoundToInt(Gravitydmg));
           
            Gravitydmg = -7; //reset des dmg
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

    private void SpectatorFall()
    {
       
        JumpForce = ForceMode.Impulse;
        rb.AddForce(0,-Jump,0,JumpForce); 
        
    }
    private void PlayerJump()
    {
        hasJumped = true;
        JumpForce = ForceMode.Impulse;
        rb.AddForce(0,Jump,0,JumpForce); //le type de force
        
    }
    
 
    
    
}
