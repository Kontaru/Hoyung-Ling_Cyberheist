using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Sentinel : BaseEnemy {

	// Use this for initialization
	override public void Start () {

        BL_startAsCombat = true;
	}

    [Command]
    public override void Cmd_FireBullet()
    {
        base.Cmd_FireBullet();

        var _bullet = (GameObject)Instantiate(go_projectilePrefab, transform.position + transform.TransformDirection(new Vector3(1, 1, 1.5F)), transform.rotation);

        NetworkServer.Spawn(_bullet);

        _bullet = (GameObject)Instantiate(go_projectilePrefab, transform.position + transform.TransformDirection(new Vector3(-1, 1, 1.5F)), transform.rotation);

        NetworkServer.Spawn(_bullet);
    }
}
