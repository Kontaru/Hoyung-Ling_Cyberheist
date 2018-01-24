using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : Entity
{

    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public bool localPlayer = false;
    public string newSceneName;

    void Start()
    {
        //Telling the GameManager who the players are
        for (int i = 0; i < GameManager.instance.GO_Player.Length; i++)
        {
            //For the current index, if it's empty
            if (GameManager.instance.GO_Player[i] == null)
            {
                //Make the gameobject this, and quit the cycle of sadness
                GameManager.instance.GO_Player[i] = gameObject;
                Debug.Log("Player added");
                break;
            }
        }

        //If the camera is local player
        if (isLocalPlayer)
        {
            //From the camera follow script, set the reference to this game object.
            CameraFollow.PlayerRef = gameObject;
        }
    }

    void Update()
    {
        //This will break the loop if the game doesn't recognise the gameobject as a local player.
        if (!isLocalPlayer)
        {
            return;
        }
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CmdFire();
        }

    }

    //Command is run by the client, calls the command, and tells the server to do it
    [Command]
    void CmdFire()
    {
        // Create the Bullet from the Bullet Prefab
        var bullet = (GameObject)Instantiate(
            bulletPrefab,
            bulletSpawn.position,
            bulletSpawn.rotation);

        // Add velocity to the bullet
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6;

        //Spawn the bullet on the clients
        NetworkServer.Spawn(bullet);
        AudioManager.instance.Play("Gas gun");

        // Destroy the bullet after 2 seconds
        Destroy(bullet, 2.0f);
    }

    public override void OnStartLocalPlayer()
    {
        transform.GetChild(0).GetComponent<MeshRenderer>().material.color = Color.blue;
    }
}
