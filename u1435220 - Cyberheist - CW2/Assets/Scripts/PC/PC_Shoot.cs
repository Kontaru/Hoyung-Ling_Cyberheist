using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PC_Shoot : NetworkBehaviour
{
    [Header("Bullet Types")]
    public GameObject bulletPrefab;

    [Header("Shooting Param")]
    public Transform bulletSpawn;

    public bool BL_Staggered = false;

    void Update()
    {


    }

    //Command is run by the client, calls the command, and tells the server to do it

}
