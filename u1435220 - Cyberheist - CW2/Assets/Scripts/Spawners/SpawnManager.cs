using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SpawnManager : NetworkBehaviour
{
    public static SpawnManager instance;

    public GameObject enemyPrefab;
    public SpawnObject[] spawns;
    SpawnObject current;

    float cooldown = 0;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(transform.gameObject);
    }

    void Update()
    {
        if (!isServer) return;

        if (GameManager.instance.enemycount == GameManager.instance.max_enemies) return;

        if (Time.time > cooldown + 5.0f)
        {
            ChoosePosition();

            var enemy = (GameObject)Instantiate(current.enemyPrefab, current.pos, current.rot);
            GameManager.instance.enemycount++;
            NetworkServer.Spawn(enemy);
            cooldown = Time.time;
        }
    }

    void ChoosePosition()
    {
        SpawnObject newspawn = spawns[Random.Range(0, spawns.Length)];
        current = newspawn;
    }
}
