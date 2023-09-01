using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Timer : MonoBehaviour
{
    int maxTimer;
    int curTime = 0;

    private void StertSell()
    {
        StartCoroutine(StartTimer());
    }
    IEnumerator StartTimer()
    {
        yield return null;

        curTime += (int)Time.time;
        
    }
}
