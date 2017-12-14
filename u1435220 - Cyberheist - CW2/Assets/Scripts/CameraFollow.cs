using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public static GameObject PlayerRef;

	// Use this for initialization
	void Update () {
        //If the player ref is stated, do the follow
        if (PlayerRef != null)
            Follow(PlayerRef);
	}

    //Parents an object to another thing
    void Follow(GameObject parentGo)
    {
        //Sets the position of this gameobject to the "parent"
        gameObject.transform.position = parentGo.transform.position;
    }
}
