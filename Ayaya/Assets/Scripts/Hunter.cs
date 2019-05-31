using System.Collections;
using System.Collections.Generic;
using trucs_perso;
using UnityEngine;

public class Hunter : Player
{
    private Camera camera;

    private void Start()
    {
        gameObject.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(true);
        gameObject.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
        camera = gameObject.transform.GetChild(0).transform.GetChild(1).GetComponentInChildren<Camera>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        maxHP = 100;
        currentHP = maxHP;
    }
}
