using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathTimer : MonoBehaviour {

    public Image HealthBar;
    private RectTransform RT_HealthBar;

    public float TimeLeft;
    float StoredTime;
    public float scale = 1f;
    float FL_TimeBarWidth;

    // Use this for initialization
    void Start () {
        StoredTime = TimeLeft;
        RT_HealthBar = HealthBar.rectTransform;
        FL_TimeBarWidth = RT_HealthBar.sizeDelta.x;
        StartCoroutine(DeathChoir(TimeLeft - 7.3f));

        AudioManager.instance.Play("Bell");
    }
	
	// Update is called once per frame
	void Update () {

        #region --- Time ---

        TimeLeft -= Time.deltaTime;
        scale = (float)TimeLeft / (float)StoredTime;

        RT_HealthBar.sizeDelta = new Vector2(FL_TimeBarWidth * scale, RT_HealthBar.sizeDelta.y);

        if (TimeLeft <= 0)
        {
            GameManager.instance.BL_Die = true;
            TimeLeft = StoredTime;
        }

        
        #endregion
    }

    IEnumerator DeathChoir(float delay)
    {
        yield return new WaitForSeconds(delay);
        AudioManager.instance.Play("Death");
    }
}
