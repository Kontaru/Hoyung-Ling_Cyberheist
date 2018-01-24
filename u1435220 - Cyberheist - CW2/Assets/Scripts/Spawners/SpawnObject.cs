using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour {

    public GameObject enemyPrefab;
    public Vector3 pos;
    public Quaternion rot;
    public int currentCount;
    public int spawnLimit;

	// Use this for initialization
	void Start () {
        pos = transform.position;
        rot = transform.rotation;
	}

    void Update()
    {
        if(currentCount == spawnLimit)
        {
            StartCoroutine(Reset());
        }
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(5.0f);
        currentCount = 0;
    }
}
