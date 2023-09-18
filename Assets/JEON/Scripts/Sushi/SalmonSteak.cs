using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SalmonSteak : MonoBehaviour
{
    int grilling;

    int currentScore;

    [SerializeField] Material goodGril;
    [SerializeField] Material burncGril;

    Coroutine grillingSteak;

    public void GrillingSteak() 
    {
        Debug.Log("구워지는중");
        grillingSteak = StartCoroutine(Grilling());
    }

    public void ExitSteak()
    {
        StopCoroutine(grillingSteak);
    }
    IEnumerator Grilling()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            grilling++;
            Debug.Log($"{grilling}");
            if (grilling == 10)
            {
                gameObject.GetComponent<MeshRenderer>().material = goodGril;

                currentScore = 5000;
            }
            else if (grilling >= 15)
            {
                Debug.Log($"넘었다");
                gameObject.GetComponent<MeshRenderer>().material = burncGril;

                currentScore = 0;

                StopCoroutine(grillingSteak);
            }
        }
    }
}
