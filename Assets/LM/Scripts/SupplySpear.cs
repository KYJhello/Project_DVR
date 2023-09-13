using LM;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SupplySpear : MonoBehaviour
{
    [SerializeField] Spear spear;
    enum Spear { Attack, Return }
    XRSocketInteractor socket;
    Coroutine refill;

    private void Awake()
    {
        socket = GetComponent<XRSocketInteractor>();
    }
    private void OnEnable()
    {
        refill = StartCoroutine(Refill());
    }
    private void OnDisable()
    {
        StopCoroutine(refill);
    }
    IEnumerator Refill()
    {
        yield return new WaitForSeconds(3);
        while (true) 
        {
            if(!socket.hasSelection)
            {
                if(spear == Spear.Attack)
                    GameManager.Resource.Instantiate<AttackSpear>("AttackSpear", transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
                else if(spear == Spear.Return)
                    GameManager.Resource.Instantiate<ReturnSpear>("ReturnSpear", transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
            }
            yield return new WaitForSeconds(1);
        }
    }
}