using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PC_Health : NetworkBehaviour {

    public const int maxHealth = 100;

    public bool destroyOnDeath = true;

    //Calls a function whenever this value changes.
    [SyncVar(hook = "OnChangeHealth")]
    public int currentHealth = maxHealth;
    public RectTransform healthBar;

    public void TakeDamage(int amount)
    {
        if (!isServer)
        {
            return;
        }

        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            if (destroyOnDeath)
            {
                Destroy(gameObject);
            }
            else
            {
                StartCoroutine(Respawn());
            }

        }
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(2.0f);
        currentHealth = maxHealth;
        RpcRespawn();
    }

    //This command is called by the client, and tells the server to do it.
    [ClientRpc]
    void RpcRespawn()
    {
        if (isLocalPlayer)
        {
            // move back to zero location
            transform.position = Vector3.zero;
        }
    }

    //This is a syncvar hook. When the syncvar ever changes value, this function will be called.
    void OnChangeHealth(int health)
    {
        healthBar.sizeDelta = new Vector2(health, healthBar.sizeDelta.y);
    }

    void TakeHealth(int heal)
    {
        currentHealth += heal;
        AudioManager.instance.Play("Heal");
    }
}
