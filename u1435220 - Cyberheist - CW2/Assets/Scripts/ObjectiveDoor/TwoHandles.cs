using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoHandles : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Entity>().EntityType == Entity.Entities.Player)
        {

        }
    }
}
