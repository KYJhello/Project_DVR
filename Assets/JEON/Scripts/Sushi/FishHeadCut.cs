using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishHeadCut : MonoBehaviour
{
    public GameObject self;

    Jeon.StoreFish storeFish;

    private void Awake()
    {
        self = gameObject.transform.parent.transform.parent.gameObject;
    }

    private void Start()
    {
        storeFish = self.GetComponent<Jeon.StoreFish>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 25)
        {
            storeFish.HeadCutting();
        }
    }
}
