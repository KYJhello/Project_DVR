using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private TextMesh textMesh;

    private int curTime;
    private int minute;
    private int second;

    private void Awake()
    {
        textMesh = GetComponent<TextMesh>();
    }
    public void StertSell()
    {
        minute = 5;
        second = 0;
        StartCoroutine(StartTimer());
    }
    IEnumerator StartTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (second < 0)
            {
                minute -= 1;
                second = 59;
            }
            second -= 1;
        }
    }
}
