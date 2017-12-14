using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Images
{
    public Image image;
    public float when;
    public float howlong;
    public bool trigger;

    public Color alphaStart;
    public Color alphaEnd;

    public bool Reversible = false;

    public float interval = 0;
    float speed = 1;

    [HideInInspector]
    public bool stopchecking = false;

    public Images()
    {
        image = null;
        when = 0;
        howlong = 2;
        trigger = false;
    }

    public Images(Image pic, float startat, float duration, bool beginstate, Color start, Color end)
    {
        image = pic;
        when = startat;
        howlong = duration;
        trigger = beginstate;
        alphaStart = start;
        alphaEnd = end;
    }

    public void Animate()
    {
        if (trigger)
        {
            if (howlong == 0)
                image.color = alphaEnd;
            else
            {
                image.color = Vector4.Lerp(alphaStart, alphaEnd, interval / howlong);
                interval += Time.deltaTime * speed;


                if (interval / howlong >= 1.0f)
                {
                    if (Reversible)
                        speed = -speed;
                    else
                        trigger = !trigger;
                }

                if (interval / howlong <= 0.0f && Reversible)
                {
                    speed = -speed;
                    trigger = !trigger;
                }
            }
        }
    }

    public void AddWait(float wait)
    {
        when += wait;
    }
}

public class ImageFading : MonoBehaviour {

    public Images[] LayerList;

    [Header("Animation Params")]
    public float currentime;
    public bool startanimation = false;
    float waitingtime = 0;

	// Use this for initialization
	void Start () {
        currentime = 0;

        foreach (Images i in LayerList)
        {
            i.image.color = i.alphaStart;
            if (i.Reversible)
                waitingtime = i.when + i.howlong;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (startanimation == true)
            currentime += Time.deltaTime;

        foreach (Images i in LayerList)
        {
            if (currentime >= i.when)
            {
                if (i.stopchecking == false)
                {
                    i.trigger = true;
                    if(i.Reversible)
                    {
                        i.AddWait(waitingtime);
                    }else
                    {
                        i.stopchecking = true;
                    }
                }
            }

            i.Animate();
        }
	}

    public void BeginFade()
    {
        startanimation = !startanimation;
    }
}
