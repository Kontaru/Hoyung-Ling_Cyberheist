using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PC_Controller : Entity
{

    //---------------------------------------------------------------------------
    //----- The brain of the PC: handles all the other functions in its system
    //---------------------------------------------------------------------------

    //Bool

    public bool BL_Staggered = false;
    public bool BL_StaggerToggle = false;
    public float FL_StaggerTimer = 1.0f;
    public bool BL_IsMoving;

    //Controllers
    PC_Shoot CC_Shoot;
    PC_Move CC_Move;

    // Use this for initialization
    void Start()
    {

        //When the player exists in the scene, the player will stop the theme and start playing the game BGM.
        AudioManager.instance.Stop("Theme");
        AudioManager.instance.Play("Game BGM");

        //Grab some components - these components are what the brain wants to control
        CC_Shoot = GetComponent<PC_Shoot>();
        CC_Move = GetComponent<PC_Move>();

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

    // Update is called once per frame
    void Update()
    {

        BL_IsMoving = CC_Move.BL_Moving;
        //if (CC_Shoot.BL_Staggered == true)
        //{
        //    BL_Staggered = CC_Shoot.BL_Staggered;
        //    CC_Move.BL_Staggered = BL_Staggered;
        //}

        //if (CC_Move.BL_Staggered == true)
        //{
        //    BL_Staggered = CC_Move.BL_Staggered;
        //    CC_Melee.BL_Staggered = BL_Staggered;
        //}

        //if (BL_Staggered)
        //{
        //    if (BL_StaggerToggle == false)
        //    {
        //        StartCoroutine(StaggerTimer(FL_StaggerTimer));
        //        BL_StaggerToggle = true;
        //    }
        //}
    }

    IEnumerator StaggerTimer(float delay)
    {
        yield return new WaitForSeconds(delay);
        BL_Staggered = false;
        CC_Move.BL_Staggered = BL_Staggered;
        BL_StaggerToggle = false;
    }

    public override void OnStartLocalPlayer()
    {
        transform.GetChild(0).GetComponent<MeshRenderer>().material.color = Color.blue;
    }
}
