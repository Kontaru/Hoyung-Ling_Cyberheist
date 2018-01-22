using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket_Bullet : Bullet {

    //---------------------------------------------------------------------------
    //----- Rocket bullets: picks up speed and hurts all (don't use in CQC!)
    //---------------------------------------------------------------------------

    float speedMultiplier = 1.0f;
    float exponentialIncrease = 2.0f;

    void Update()
    {
        RB_Bullet.velocity = Vector3.Lerp
            (transform.TransformDirection(Vector3.forward) * speed
            , transform.TransformDirection(Vector3.forward) * speed * 100
            , Time.deltaTime * speedMultiplier);

        speedMultiplier += 0.001f * exponentialIncrease;
        if(speedMultiplier > 0.5f)
            exponentialIncrease += Time.deltaTime * 1000f;
    }

    //Inherited method
    override public void SendDamage(Collider coll)
    {
        //If there is a collider
        if (coll != null)
        {
            if (coll.gameObject.GetComponent<Entity>() != null)
            {
                //If the collider is player
                if (coll.gameObject.GetComponent<Entity>().EntityType == Entity.Entities.Player)
                {
                    //Send damage depending on what kind of enemy has been hit
                    coll.gameObject.SendMessage("TakeDamage", damage / 2, SendMessageOptions.DontRequireReceiver);
                    //Destroy the bullet
                    Destroy(gameObject);
                }
                else if (coll.gameObject.GetComponent<Entity>().EntityType == Entity.Entities.Enemy)
                {
                    //Send damage depending on what kind of enemy has been hit
                    coll.gameObject.SendMessage("TakeDamage", damage * 2, SendMessageOptions.DontRequireReceiver);
                    //Destroy the bullet
                    Destroy(gameObject);
                }
                else
                {
                    Debug.Log("Enemy Bullet: Outside Loop");
                    Destroy(gameObject);
                }
            }else
            {
                Destroy(gameObject);
            }
        }
    }
}
