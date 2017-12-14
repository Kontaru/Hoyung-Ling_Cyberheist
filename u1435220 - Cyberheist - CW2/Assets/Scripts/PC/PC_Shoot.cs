using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PC_Shoot : NetworkBehaviour
{
    [Header("Bullet Types")]
    public GameObject bulletPrefab;
    public GameObject missilePrefab;

    [Header("Shooting Param")]
    public Transform bulletSpawn;

    public bool BL_Staggered = false;

    void Update()
    {
        

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CmdFire(bulletPrefab);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            CmdFire(missilePrefab);
        }

    }

    //Command is run by the client, calls the command, and tells the server to do it
    [Command]
    void CmdFire(GameObject projectile)
    {
        // Generic code to spawn a bullet from an ejection point
        var bullet = (GameObject)Instantiate(
            projectile,
            bulletSpawn.position,
            bulletSpawn.rotation);

        //Spawn the bullet on the clients
        NetworkServer.Spawn(bullet);
    }
}
