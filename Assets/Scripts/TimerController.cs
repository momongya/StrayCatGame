using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    float timer = 30f;
    public Text timerText;

    void Start()
    {
    
    }

    void Update()
    {
        timer -= Time.deltaTime;
        timerText.text = timer.ToString("残り時間 : " + "0" + "秒");
    }
}
