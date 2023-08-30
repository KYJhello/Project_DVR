using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScripts : MonoBehaviour
{
    Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(FishMoveRoutine());
    }
    float forceGravity = 0.01f;
    private void FixedUpdate()
    {
        rb.AddForce(Vector3.down * forceGravity);
    }

    IEnumerator FishMoveRoutine()
    {
        while (true)
        {
            rb.AddForce(Vector3.up * 5f, ForceMode.Impulse);
            yield return new WaitForSeconds(1f);
        }
    }

}
