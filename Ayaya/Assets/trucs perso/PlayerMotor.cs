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
    private int Jump = 10; //à modifier
    private ForceMode JumpForce; 
    bool isGrounded = false;
    public Player player;
    private float Gravitydmg = -15;
    [SerializeField] private float cameraRotationLimit = 85f; //Permet d'empecher le bug de cam 
    //mathieu 
    public GameObject transformer; 
    float yRot;
    float xRot;
    private bool IsMoving;
    float lookSensitivity = 3f;
    private float notmovingtime;
    private List<AudioSource> taunts;
    RaycastHit raytransfo;  
    //mathieu
   

    private void Start()
    {
        notmovingtime = 20;
        
        taunts = GameObject.Find("Taunts").GetComponentsInChildren<AudioSource>().ToList();
        
        player = GetComponentInParent<Player>();
        rb = GetComponent<Rigidbody>(); //on implémente le rigidbody au début
        //mathieu
        if (!transformer)
            transformer = GameObject.FindGameObjectWithTag("Player");
        rb = transformer.GetComponent<Rigidbody>(); 
        transformer.isStatic = false;
        //mathieu
    }

    public void Move(Vector3 velocity)
    {
        this.velocity = velocity; //on modifie le déplacement
    }

    void OnCollisionEnter(Collision collision) //est appelée quand le joueur touche le sol
    {
       
        if (collision.gameObject.CompareTag("Ground"))
        {     
            isGrounded = true;
        }
    }

    private void Update()
    {Debug.Log(IsMoving);
        Debug.Log(notmovingtime);
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
            taunts[randomtaunt.Next(taunts.Count)].Play();
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
   
    private void LateUpdate()//mathieu
        { 
            if (Input.GetKeyDown(KeyCode.R)&&Physics.Raycast(transformer.transform.position, camera.transform.forward, out raytransfo, 10))
                transfo(raytransfo, ref transformer);
        } 
   
    public void transfo(RaycastHit cible, ref GameObject trans)//mathieu 
        { 
            if (trans.GetComponent<BoxCollider>()) 
                Destroy(trans.GetComponent<BoxCollider>()); 
            if (trans.GetComponent<SphereCollider>()) 
                Destroy(trans.GetComponent<SphereCollider>()); 
            if (cible.collider.gameObject.GetComponent<BoxCollider>()) 
            { 
                trans.AddComponent<BoxCollider>(); 
                trans.GetComponent<BoxCollider>().size = cible.collider.gameObject.GetComponent<BoxCollider>().size; 
                trans.GetComponent<BoxCollider>().center = cible.collider.gameObject.GetComponent<BoxCollider>().center; 
            } 
            if (cible.collider.gameObject.GetComponent<SphereCollider>()) 
            { 
                trans.AddComponent<SphereCollider>(); 
                trans.GetComponent<SphereCollider>().radius = cible.collider.gameObject.GetComponent<SphereCollider>().radius; 
                trans.GetComponent<SphereCollider>().center = cible.collider.gameObject.GetComponent<SphereCollider>().center; 
            } 
            Destroy(trans.GetComponent<Mesh>()); 
            trans.AddComponent<MeshFilter>(); 
            trans.GetComponent<MeshFilter>().mesh = cible.collider.gameObject.GetComponent<MeshFilter>().mesh; 
            trans.transform.position.Set(trans.transform.position.x, cible.collider.gameObject.transform.position.y, trans.transform.position.z); 
        } 
    
    private void PerformMovement()
    {
        if (velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position+velocity * Time.fixedDeltaTime);
            
            notmovingtime = 10;
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
