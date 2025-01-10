using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TimeCounter : MonoBehaviour
{
    public float TimeCountSec;
    public int TimeCountMin;
    public Text Timer;
    void Update()
    {
        //Timer.text = Mathf.Round(TimeCountSec).ToString();
        TimeCount();
    }
    private void TimeCount()
    {
        TimeCountSec += Time.deltaTime;
        Timer.text = string.Format("{0:D2}:{1:D2}", TimeCountMin, (int)TimeCountSec);
        if (TimeCountSec > 59f)
        {
            TimeCountSec = 0;
            TimeCountMin++;
        }
    }
}