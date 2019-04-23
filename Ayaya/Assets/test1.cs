using UnityEngine; 
 
[RequireComponent(typeof(Rigidbody))] 
 
public class teste1 : MonoBehaviour 
{ 
    public Camera cam; 
    public GameObject transformer; 
    float yRot;
    float xRot;
    float lookSensitivity = 3f;
    Rigidbody rb;
    RaycastHit raytransfo;
 
    private void Start() 
    { 
        transformer = GameObject.FindGameObjectWithTag("Player"); 
        cam = GameObject.FindGameObjectWithTag("Camera").GetComponent<Camera>(); 
        rb = transformer.GetComponent<Rigidbody>(); 
        transformer.isStatic = false; 
    } 
     
    void FixedUpdate() 
    { 
        yRot = Input.GetAxisRaw("Mouse X"); 
        xRot = Input.GetAxisRaw("Mouse Y"); 
        rb.MoveRotation(rb.rotation * Quaternion.Euler(new Vector3(0, yRot, 0) * lookSensitivity)); 
        cam.transform.Rotate(new Vector3(-xRot, 0, 0) * lookSensitivity); 
    } 
 
    private void LateUpdate() 
    { 
        if (Input.GetKeyDown(KeyCode.R)&&Physics.Raycast(transformer.transform.position, cam.transform.forward, out raytransfo, 10)) 
        { 
            transfo(raytransfo, ref transformer); 
        } 
    } 
 
    public void transfo(RaycastHit cible, ref GameObject trans) 
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
}