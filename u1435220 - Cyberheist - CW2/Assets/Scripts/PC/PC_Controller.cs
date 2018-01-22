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

    public bool BL_Moving;
    Rigidbody RB_PC;
    Vector3 direction;

    ActiveGun activeGun;

    [Header("Movement")]
    float FL_moveSpeed = 20f;
    float FL_defaultSpeed;

    // Client Variables
    [SyncVar]
    private Vector3 V3_syncPos;
    [SyncVar]
    private float FL_syncYRot;
    private float FL_lerpRate = 10f;

    public override void OnStartLocalPlayer()
    {
        transform.GetChild(0).GetComponent<MeshRenderer>().material.color = Color.blue;
    }

    // Use this for initialization
    void Start()
    {
        if (isServer)
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
        }

        ////For the current index, if it's empty
        //if (GameManager.instance.GO_Player[0] == null)
        //{
        //    //Make the gameobject this, and quit the cycle of sadness
        //    GameManager.instance.GO_Player[0] = gameObject;
        //    Debug.Log("Player added");
        //}
        //else if (GameManager.instance.GO_Player[1] == null)
        //{
        //    //Make the gameobject this, and quit the cycle of sadness
        //    GameManager.instance.GO_Player[0] = gameObject;
        //    Debug.Log("Player added");
        //}
        //else
        //{
        //    Debug.Log("No player added: too many players");
        //}

        //If the camera is local player
        if (isLocalPlayer)
        {
            //From the camera follow script, set the reference to this game object.
            CameraFollow.PlayerRef = gameObject;
        }else if (!isLocalPlayer)
        {
            return;
        }

        activeGun = GetComponent<ActiveGun>();
        RB_PC = GetComponent<Rigidbody>();
        FL_defaultSpeed = FL_moveSpeed;

        //When the player exists in the scene, the player will stop the theme and start playing the game BGM.
        AudioManager.instance.Stop("Theme");
        AudioManager.instance.Play("Game BGM");
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        PlayerMove();
        LookInput();

        activeGun.Shoot(KeyCode.Space);

        if (Input.GetKeyDown(KeyCode.Q))
        {
            activeGun.Missile();
        }
    }

    private void FixedUpdate()
    {
        RB_PC.MovePosition(transform.position + direction * Time.fixedDeltaTime);
    }

    void PlayerMove()
    {
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        direction = moveInput.normalized * FL_moveSpeed;
    }

    void LookInput()
    {
        //Look Input
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane grounPlane = new Plane(Vector3.up, Vector3.up);
        float rayDistance;

        if (grounPlane.Raycast(ray, out rayDistance))
        {
            Vector3 point = ray.GetPoint(rayDistance);
            Vector3 heightCorrectedPoint = new Vector3(point.x, transform.position.y, point.z);
            transform.LookAt(heightCorrectedPoint);
        }
    }
}
