using DefaultNamespace;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Utility;

[RequireComponent(typeof(Rigidbody))]


public class PlayerControler : MonoBehaviour
{
   
   public float speed = 4f;
   public float SprintCooldown;
   public float stamina = 100;
   private float actualspeed;
   [SerializeField] private float LookSensibility = 6f;//à modifier
   private PlayerMotor motor;
   public bool IsRunning;
   private Rigidbody rb;
   public bool IsAiming = false;
   private void Start()
   {
       actualspeed = speed;         
       motor = GetComponent<PlayerMotor>();
       if (PlayerPrefs.HasKey("sensibilité"))
           LookSensibility =PlayerPrefs.GetFloat("sensibilité")*10+3;
       else
       {
           PlayerPrefs.SetFloat("sensibilité",LookSensibility);
       }

   }
   public void Aim()
   {
    Camera camera= GetComponentInChildren<Camera>();
      
      
       if (Input.GetButtonDown("Fire2") && !IsAiming)
       {
           camera.fieldOfView -=25;
           LookSensibility =(PlayerPrefs.GetFloat("sensibilité")*10+3)/5;
           IsAiming = true;
       }
       else
       {
           if (Input.GetButtonDown("Fire2") && IsAiming)
           {
               
               camera.fieldOfView = 50;
               IsAiming = false;

           }
       }
   }
   private void Update()
   { 
       IsRunning = false;
       bool IsMoving = false; 
       if(!IsAiming)
           LookSensibility = PlayerPrefs.GetFloat("sensibilité")*10+3;

      
     
       Vector3 moveVertical = Vector3.zero;
       Vector3 moveHorizontal = Vector3.zero; //Modification du script pour fonctionner avec l'input manager
       float xMov = 1;
       //Vecteurs : -1 = gauche, 1 = droite. 

       float zMov = 1;
       //Vecteurs: -1 = recule, 1 = avance.

       if (stamina <= 0)
           SprintCooldown = 5f;

       if (SprintCooldown > 0)
           SprintCooldown -= Time.deltaTime;
       if(Input.GetKey((KeyCode) System.Enum.Parse(typeof(KeyCode),PlayerPrefs.GetString("RunKey", "E")))&&stamina>0&&SprintCooldown<=0&&!IsAiming)
       {
           IsRunning = true;
           actualspeed = speed * 3f;
         
       }
       else
       {
           actualspeed = speed;          
       }
       if (Input.GetKey((KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("ForwardKey", "Z")))) 
           moveVertical = transform.forward * zMov;
       if(Input.GetKey((KeyCode) System.Enum.Parse(typeof(KeyCode),PlayerPrefs.GetString("BackwardKey", "S"))))
           moveVertical = transform.forward * -zMov;


       if  (Input.GetKey((KeyCode) System.Enum.Parse(typeof(KeyCode),PlayerPrefs.GetString("RightKey", "D"))))
       {
           moveHorizontal = transform.right * xMov;
           
       }
       if (Input.GetKey((KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("LeftKey", "Q"))))
            
           moveHorizontal = transform.right * -xMov;

       Aim();
      
       Vector3 velocity = (moveHorizontal + moveVertical).normalized * actualspeed;

       IsMoving = IsRunning && velocity != Vector3.zero;
       if (IsMoving) //si le joueur utilise sa touche de sprint mais ne fait aucun mouvement alors il ne perds pas de stamina
       {   
           stamina -= 0.15f;
       }
       if(stamina<100&&!IsMoving)
           stamina += 0.1f;
       
               
       //explication simple = théorème de Pythagore pour calculer le mouvement/vélocité du joueur.

       motor.Move(velocity);

       float yRot = Input.GetAxisRaw("Mouse X");
       
       
       Vector3 rotation = new Vector3(0,yRot, 0)*LookSensibility;      
       motor.Rotate(rotation);
       
       float xRot = Input.GetAxisRaw("Mouse Y");
       float camerarotationX = xRot*LookSensibility;             
       motor.RotateCamera(camerarotationX);        
   }


}