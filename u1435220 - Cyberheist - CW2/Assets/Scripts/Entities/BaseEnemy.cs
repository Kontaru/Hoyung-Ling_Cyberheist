using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

public class Target
{
    public GameObject reference;
    public float dist;
}
public class BaseEnemy : Entity {

    Target[] targets = new Target[2];

    public enum State
    {
        Idle,
        Hunt,
        Patrol,
        Combat
    }

    NavMeshAgent nav_Agent;
    public GameObject go_Target;

    [SyncVar]
    public Vector3 v_Target;

    [SyncVar]
    private Vector3 v3_syncPos;
    [SyncVar]
    private float fl_syncYRot;

    // Use this for initialization
    void Start ()
    {
        nav_Agent = GetComponent<NavMeshAgent>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        //Search all players for their distance
        for (int i = 0; i < GameManager.instance.GO_Player.Length; i++)
        {
            if (GameManager.instance.GO_Player[i] == null) break;
            else
            {
                targets[i].reference = GameManager.instance.GO_Player[i];
                targets[i].dist = Vector3.Distance(transform.position, GameManager.instance.GO_Player[i].transform.position);
            }
        }

        RpcHunt();
	}

    //Hunt Mode
    [ClientRpc]
    void RpcHunt()
    {
        //Distance of player from enemy
        float[] dist = new float[2];

        dist[0] = 100;                 //Set them to a ridiculous amount, just in case the player is null
        dist[1] = 100;                 //And we may still want to hunt 1 player

        dist[0] = targets[0].dist;
        dist[1] = targets[1].dist;

        //After grabbing the distance of all our players
        //If the distance of player 1 is lower than player 2, make him the target
        if (dist[0] < dist[1])
        {
            //Set target Vector to the player's position
            //But know who the player is
            v_Target = GameManager.instance.GO_Player[0].transform.position;
            go_Target = GameManager.instance.GO_Player[0];
            Debug.Log("P1 Target");
        }
        //If the distance of player 2 is lower than player 1, make him the target
        else if (dist[1] < dist[0])
        {
            v_Target = GameManager.instance.GO_Player[1].transform.position;
            go_Target = GameManager.instance.GO_Player[0];
            Debug.Log("P2 Target");
        }
        //If either the two players are the same distance, prioritise player 1 (unlikely to happen)
        else if (dist[0] == dist[1])
        {
            v_Target = GameManager.instance.GO_Player[0].transform.position;
            go_Target = GameManager.instance.GO_Player[0];
            Debug.Log("Hunt: Values are equal");
        }

        nav_Agent.destination = v_Target;
    }
}
