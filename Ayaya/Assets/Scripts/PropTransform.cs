using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropTransform : MonoBehaviour
{
    float posx;
    float posy;
    float posz;
    Vector3 pos;
    bool boxShape;
    public GameObject transformer;
    RaycastHit raytransfo;

    private void Start()
    {
        transformer = GameObject.FindGameObjectWithTag("Player");
        boxShape = true;
        transformer.isStatic = false;
        transformer.AddComponent<Rigidbody>();
    }

    void Update()
    {
        posx = transformer.GetComponent<Rigidbody>().position.x;
        posy = transformer.GetComponent<Rigidbody>().position.y;
        posz = transformer.GetComponent<Rigidbody>().position.z;
      
    }

    private void LateUpdate()
    {
        
        if (Input.GetKeyDown(KeyCode.T)&&Physics.Raycast(transformer.transform.position, transformer.transform.forward, out raytransfo, 10))
        {
             transfo_v2(raytransfo, ref transformer);
        }
    }
public void transfo_v2(RaycastHit cible, ref GameObject trans)
    {
        if (trans.GetComponent<BoxCollider>() != null)
            Destroy(trans.GetComponent<BoxCollider>());
        if (trans.GetComponent<SphereCollider>() != null)
            Destroy(trans.GetComponent<SphereCollider>());
        if (cible.collider.gameObject.GetComponent<BoxCollider>() != null)
        {
            trans.AddComponent<BoxCollider>();
            trans.GetComponent<BoxCollider>().size = cible.collider.gameObject.GetComponent<BoxCollider>().size;
            trans.GetComponent<BoxCollider>().center = cible.collider.gameObject.GetComponent<BoxCollider>().center;
        }
        if (cible.collider.gameObject.GetComponent<SphereCollider>() != null)
        {
            trans.AddComponent<SphereCollider>();
            trans.GetComponent<SphereCollider>().radius = cible.collider.gameObject.GetComponent<SphereCollider>().radius;
            trans.GetComponent<SphereCollider>().center = cible.collider.gameObject.GetComponent<SphereCollider>().center;
        }
        Destroy(trans.GetComponent<Mesh>());
        Destroy(trans.GetComponent<MeshFilter>());
        trans.AddComponent<MeshFilter>();
        Destroy(trans.GetComponent(GameObject.FindWithTag("Model").name));
        trans.GetComponent<MeshFilter>().mesh = cible.collider.gameObject.GetComponent<MeshFilter>().mesh;
        trans.transform.position.Set(trans.transform.position.x, cible.collider.gameObject.transform.position.y, trans.transform.position.z);
    }
}