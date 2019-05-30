using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
[System.Serializable] //permet à Unity de charger cette classe et donc de pouvoir la modifier via Unity

//Tout les détails de l'arme.     

public class PlayerWeapon : MonoBehaviour
{
    private string name = "AK-47";
    public int dmg = 10;//à modifier
     public float range = 100f;//à modifier
     public int ammo = 40;
     public int chargercapacity = 40;
     public float WeaponCooldown = 0.1f;
     public float WeaponActualCooldown = 0.3f;


}