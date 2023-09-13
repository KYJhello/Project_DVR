using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RawSalmon : MonoBehaviour
{
    float cuttingCount;

    GameObject knife;

    public List<GameObject> fishMeats;
    public List<Collider> orderOfCutting;

    private void Awake()
    {
        knife = GameObject.Find("Knife");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 25)
        {
            Debug.Log("µé¾î¿È");
            cuttingCount++;
            if (cuttingCount >= 5)
            {
                Debug.Log("ÄÆ");

                foreach (GameObject salmon in fishMeats)
                {
                    salmon.SetActive(true);
                    salmon.transform.SetParent(null);
                }
                gameObject.SetActive(false);
            }
        }

    }

    private void NextTurn()
    {
        orderOfCutting[0].enabled = true;
        
    }
}
