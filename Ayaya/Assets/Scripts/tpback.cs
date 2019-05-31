using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tpback : MonoBehaviour
{
    public GameObject player;
    public GameObject respawn;
    public BoxCollider sol;

    public void _tpback(GameObject player, GameObject respawn)
    {
        player.transform.position = respawn.transform.position;
    }

    private void Update()
    {
        if (sol.isTrigger)
        {
            _tpback(player,respawn);
        }
    }
}
