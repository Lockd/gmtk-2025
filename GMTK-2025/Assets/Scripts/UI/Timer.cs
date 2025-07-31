
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public static float timer;
    public static bool timeStarted = true;
    float timerCd = 0f;
    public TextMeshProUGUI timerText;
    private void Start()
    {
        timerText.text = OnGUI();
    }

    void Update()
    {
        if (timeStarted)
        {
            timer += Time.deltaTime;
        }
        if (Time.time >= timerCd)
        {
            timerText.text = OnGUI();
            timerCd = Time.time + 1f;
        }
    }

    string OnGUI()
    {
        int minutes = Mathf.FloorToInt(timer / 60F);
        int seconds = Mathf.FloorToInt(timer - minutes * 60);

        string niceTime = string.Format("{0:00}:{1:00}", minutes, seconds);
        return niceTime;
    }
}
