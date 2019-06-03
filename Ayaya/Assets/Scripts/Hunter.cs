using System.Collections;
using System.Collections.Generic;
using trucs_perso;
using UnityEngine;

public class Hunter : Player
{
    private void Start()
    {
        gameObject.transform.GetChild(1).gameObject.SetActive(true);
        gameObject.transform.GetChild(1).GetChild(0).gameObject.SetActive(true);
        gameObject.transform.GetChild(1).GetChild(1).gameObject.SetActive(true);
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        maxHP = 100;
        currentHP = maxHP;
    }
}
