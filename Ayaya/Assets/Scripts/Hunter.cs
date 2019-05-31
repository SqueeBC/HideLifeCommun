using System.Collections;
using System.Collections.Generic;
using trucs_perso;
using UnityEngine;

public class Hunter : Player
{
    private void Start()
    {    
           
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        maxHP = 100;
        currentHP = maxHP;
        
    }

}
