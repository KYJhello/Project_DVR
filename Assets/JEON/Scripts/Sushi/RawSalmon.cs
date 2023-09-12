using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RawSalmon : MonoBehaviour
{
    GameObject knife;

    public List<GameObject> fishMeats;
    public List<Collider> orderOfCutting;

    private void Awake()
    {
        knife = GameObject.Find("Knife");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == knife)
        {
            
        }
    }

    private void NextTurn()
    {
        orderOfCutting[0].enabled = true;
        
    }
}
