using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinfeRay : MonoBehaviour
{
    public LayerMask mask;
    public RaycastHit hit;
    public float maxDistance = 5f;

    bool firstHeadHit = false;
    private void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit, maxDistance, mask) && !firstHeadHit)
        {
            Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.red);

            StartCoroutine(CheckSecondHitTime());
            firstHeadHit = true;
        }
        
    }
    IEnumerator CheckSecondHitTime()
    {
        yield return new WaitForSeconds(2f);

    }
}
