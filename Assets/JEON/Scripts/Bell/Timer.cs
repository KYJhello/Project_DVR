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

    private void Awake()
    {
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
