using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFadeTo : MonoBehaviour
{

    SpriteRenderer mySprite;

    public Color alphaStart;
    public Color alphaEnd;

    public bool BeginFade = true;
    public bool Reversible = false;
    public bool Repeatable = false;

    float interval = 0;
    public float timer;
    float speed = 1;
    int count = 1;
    public int maxcount = 0;


    // Use this for initialization
    void Start()
    {
        mySprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (BeginFade)
        {
            mySprite.color = Vector4.Lerp(alphaStart, alphaEnd, interval / timer);
            interval += Time.deltaTime * speed;


            if (count < maxcount || Repeatable == true)
            {
                if (interval / timer >= 1.0f && Reversible)
                {
                    speed = -speed;
                    count++;
                }

                if (interval / timer <= 0.0f && Reversible)
                {
                    speed = -speed;
                    count++;
                }
            }
        }
    }

    public void SwitchFade()
    {
        BeginFade = !BeginFade;
    }
}
