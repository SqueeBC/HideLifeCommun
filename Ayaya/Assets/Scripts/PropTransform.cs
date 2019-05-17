using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class PropTransform : MonoBehaviour
{
    public GameObject transformer;
    RaycastHit raytransfo;

    private void Start()
    {
        transformer.isStatic = false;
    }

    private void LateUpdate()
    {
            if (Input.GetKeyDown(KeyCode.T) && Physics.Raycast(transformer.transform.position, transformer.transform.forward, out raytransfo, 10))
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
        trans.GetComponent<MeshFilter>().mesh = cible.collider.gameObject.GetComponent<MeshFilter>().mesh;
        trans.transform.position.Set(trans.transform.position.x, cible.collider.gameObject.transform.position.y, trans.transform.position.z);
    }
}