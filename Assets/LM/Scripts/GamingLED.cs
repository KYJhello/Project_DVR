using System.Collections;
using UnityEngine;

public class GamingLED : MonoBehaviour
{
    [SerializeField] Material gamingMat;
    Coroutine led;
    private void OnEnable()
    {
        led = StartCoroutine(LED());
    }
    private void OnDisable()
    {
        StopCoroutine(LED());
        gamingMat.color = Color.white;
    }
    IEnumerator LED()
    {
        Color color = Color.white;
        float t = 0;
        while (true) 
        {
            if (t <= 1)
                color = Color.Lerp(Color.white, Color.red, t);
            else if (t > 1 && t <= 2)
                color = Color.Lerp(Color.red, Color.green, t - 1);
            else if (t > 2 && t <= 3)
                color = Color.Lerp(Color.green, Color.blue, t - 2);
            else if (t > 3 && t <= 4)
                color = Color.Lerp(Color.blue, Color.red, t - 3);
            else
                t = 1;
            gamingMat.color = color;
            t += Time.deltaTime * 0.5f;
            yield return null;
        }
    }
}
