using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;
using TMPro;

public class ActiveGun : NetworkBehaviour
{
    public Transform gunPosition;
    public Weapon weapon;
    public Weapon pickup;
    public Weapon fallback;

    public GameObject missilePrefab;
    public GameObject missileEject;

    public bool triggerPulled = false;

    float coolDown = 0;

    public int currentAmmo;
    public int currentClipSize;

    // UI Params
    public static TextMeshProUGUI nameText;
    public static TextMeshProUGUI clipText;
    public static TextMeshProUGUI currentClipText;
    public static TextMeshProUGUI ammoText;

    // Use this for initialization
    void Start()
    {
        if (!isLocalPlayer)
        {
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (!isLocalPlayer)
        {
            return;
        }

        if (pickup != null)
        {
            Destroy(weapon.gameObject);
            weapon = pickup;
            pickup = null;
            Cmd_SpawnGun();
        }
        else if (fallback != null && weapon == null)
        {
            weapon = fallback;
            Cmd_SpawnGun();
        }
        else if (weapon == null)
        {
            return;
        }

        triggerPulled = CrossPlatformInputManager.GetButton("Shoot");
        weapon.shoot = triggerPulled;

        //if (GO_currentGun == null)
        //{
        //    GO_currentGun = GO_defaultGun;
        //    currentGun = GO_currentGun.GetComponent<Weapon>();
        //}
        //else if (GO_currentGun.GetComponent<Weapon>().empty)
        //{
        //    Destroy(GO_currentGun);
        //}

        //GO_currentGun.transform.position = gunPosition.position;

        //currentGun.CheckAmmo();

        //if (triggerPulled == true && currentGun.clipEmpty == false)
        //{
        //    if (coolDown < Time.time)
        //    {
        //        Cmd_FireBullet();
        //        coolDown = currentGun.ApplyCooldown(coolDown);
        //    }
        //}

        UpdateUI();
    }

    void UpdateUI()
    {
        nameText.text = weapon.gun.name;
        clipText.text = string.Format("" + weapon.gun.clipSize);
        currentClipText.text = string.Format("" + weapon.currentClipSize);
        ammoText.text = string.Format("" + weapon.currentAmmo);
    }

    public void Missile()
    {
        Cmd_FireMissile();
    }

    [Command]
    void Cmd_FireMissile()
    {
        var _bullet = (GameObject)Instantiate(missilePrefab,
            missileEject.transform.position,
            missileEject.transform.rotation);

        NetworkServer.Spawn(_bullet);
    }

    [Command]
    void Cmd_SpawnGun()
    {
        var gun = (Weapon)Instantiate(weapon, gunPosition.position, gunPosition.rotation);
        NetworkServer.Spawn(gun.gameObject);
        weapon = gun;
        weapon.transform.parent = transform;
    }
}
