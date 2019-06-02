using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quit : MonoBehaviour
{
    public void quitbtn()
    {
        Debug.Log("Has quit !");
        Application.Quit();
    }
}
