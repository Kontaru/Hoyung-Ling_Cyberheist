using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViolentShake : MonoBehaviour {

    [Header("Shake Params")]
    public float OriginalShake;
    public float Randomness;

    [Header("Text Params")]
    public string NewText;
    private string OriginalText;

    private Vector2 StartPos;
    private Vector2 ShakeLeft;
    private Vector2 ShakeRight;
    private float RandX;
    private float RandY;

    [Header("Turn On?")]
    public bool BL_TextChangerOn = true;
    public bool BL_ShakeOn = true;

    [Header("Is it changing?")]
    public bool BL_ShakeItOff = false;
    public bool BL_NewText = false;

    [Header("Ramp Up Intensity")]
    public float IncreaseIntensity;
    public bool BL_IncreaseIntensity = false;
    float currenttime = 0;
    public float intensitytimer;

	// Use this for initialization
	void Start () {
        StartPos = transform.position;
        OriginalText = transform.GetChild(0).GetComponent<Text>().text;
        ShakeLeft = new Vector2(StartPos.x - 1.0f * Randomness, StartPos.y - 1.0f * Randomness);
        ShakeRight = new Vector2(StartPos.x + 1.0f * Randomness, StartPos.y + 1.0f * Randomness);
    }
	
	// Update is called once per frame
	void Update () {

        if (BL_IncreaseIntensity)
        {
            Randomness = Mathf.Lerp(Randomness, IncreaseIntensity, currenttime / intensitytimer);
            currenttime += Time.deltaTime;
        }

        if (BL_ShakeOn)
        {
            if (BL_ShakeItOff)
            {
                SetShake(Randomness);
                RandX = Random.Range(ShakeRight.x, ShakeLeft.x);
                RandY = Random.Range(ShakeRight.y, ShakeLeft.y);
                transform.position = new Vector2(RandX, RandY);
            }
            else
            {
                if (OriginalShake == 0)
                    transform.position = StartPos;
                else
                {
                    SetShake(OriginalShake);
                    RandX = Random.Range(ShakeRight.x, ShakeLeft.x);
                    RandY = Random.Range(ShakeRight.y, ShakeLeft.y);
                    transform.position = new Vector2(RandX, RandY);
                }
            }
        }

        if (BL_TextChangerOn)
        {
            if (BL_NewText)
            {
                transform.GetChild(0).GetComponent<Text>().text = string.Format(NewText);
            }
            else
            {
                transform.GetChild(0).GetComponent<Text>().text = string.Format(OriginalText);
            }
        }
    }

    public void SwapShakeState()
    {
        BL_ShakeItOff = !BL_ShakeItOff;
    }

    public void ChangeText()
    {
        BL_NewText = !BL_NewText;
    }

    void SetShake(float shakevalue)
    {
        ShakeLeft = new Vector2(StartPos.x - 1.0f * shakevalue, StartPos.y - 1.0f * shakevalue);
        ShakeRight = new Vector2(StartPos.x + 1.0f * shakevalue, StartPos.y + 1.0f * shakevalue);
    }

    public void IntensityPlus()
    {
        BL_IncreaseIntensity = !BL_IncreaseIntensity;
    }
}
