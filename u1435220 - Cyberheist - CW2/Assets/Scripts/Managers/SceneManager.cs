using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SceneManager : NetworkBehaviour {

    public static SceneManager instance;

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

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }

    [ServerCallback]
    public void LoadOnline(string sceneName)
    {
        NetworkManager.singleton.ServerChangeScene(sceneName);
    }
}
