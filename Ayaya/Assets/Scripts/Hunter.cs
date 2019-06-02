using System.Collections;
using System.Collections.Generic;
using trucs_perso;
using UnityEngine;

public class Hunter : Player
{
    private Camera camera;
    private void Start()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        gameObject.transform.GetChild(1).gameObject.SetActive(true);
        camera = gameObject.transform.GetChild(1).gameObject.GetComponentInChildren<Camera>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        maxHP = 100;
        currentHP = maxHP;
    }
}
