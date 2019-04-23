using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Eject : MonoBehaviour
{
    private Rigidbody rb;
    private int ejectSpeed = 100;
    private float fireRate = 0.5f;
    private float nextFire = 0f;
    private bool fullAuto = false;


  

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
            nextFire = Time.time + fireRate;
        

    }
}
