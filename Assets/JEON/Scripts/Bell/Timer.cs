using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMesh[] textMesh;

    private int curTime;
    private int minute;
    [SerializeField] int second;

    private bool touchButton = true;

    private static Timer timerTime;
    private static bool close;
    public static Timer TimerTime { get { return timerTime; } }
    public static bool Close { get { return close; } }

    private void Awake()
    {
        if (timerTime != null && timerTime != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            timerTime = this;
        }

        close = true;
        textMesh = GetComponentsInChildren<TextMesh>();
    }
    public void StertSell()
    {
        if (touchButton)
        {
            minute = 3;
            second = 0;
            touchButton = false;
        }
        
        if (minute >= 3)
            StartCoroutine(StartTimer());
    }

    public void PenaltyTime()
    {
        if (second >= 11)
        {
            second -= 10;
        }
        else if (second <= 10)
        {
            minute -= 1;
            second += 50;
        }
        
    }
    public void FnishTime()
    {
        close = false;
    }

    IEnumerator StartTimer()
    {
        if (minute == 0 && second == 0)
        {
            StopCoroutine(StartTimer());
            minute = 3;
            second = 0;
        }
        while (true)
        {
            yield return new WaitForSeconds(1);

            if (second <= 0)
            {
                minute -= 1;
                second = 60;
            }

            second -= 1;

            if (second <= 9)
            {
                textMesh[0].text = ($"{minute} : ");
                textMesh[1].text = ($"0{second}");
            }
            else
            {
                textMesh[0].text = ($"{minute} : ");
                textMesh[1].text = ($"{second}");
            }
            
        }
    }
}
