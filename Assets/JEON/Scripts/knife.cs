using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class Knife : MonoBehaviour
{
    [SerializeField] int cuttingCount;

    public GameObject headCuttingPoint;
    public GameObject tailCuttingPoint;

    private void Awake()
    {
        headCuttingPoint = GameObject.Find("HeadLine");
        tailCuttingPoint = GameObject.Find("TailLine");
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject fishHead = GameObject.Find("CuttingPoint_1");
        GameObject fishTail = GameObject.Find("CuttingPoint_2");

        if (other.gameObject == headCuttingPoint)
        {
            Debug.Log("머리잘린다");
            fishHead.transform.SetParent(null);
            fishHead.GetComponent<BoxCollider>().enabled = true;
            fishHead.GetComponent<Rigidbody>().useGravity = true;
            fishHead.GetComponent<Rigidbody>().isKinematic = false;
            fishHead.GetComponent<XRGrabInteractable>().enabled = true;
            fishHead.GetComponent<XRGrabInteractable>().attachTransform = fishHead.transform;
        }
        if (other.gameObject == tailCuttingPoint)
        {
            Debug.Log("꼬리잘린다");
            fishTail.transform.SetParent(null);
            fishTail.GetComponent<BoxCollider>().enabled = true;
            fishTail.GetComponent<Rigidbody>().useGravity = true;
            fishTail.GetComponent<Rigidbody>().isKinematic = false;
            fishTail.GetComponent<XRGrabInteractable>().enabled = true;
            fishTail.GetComponent<XRGrabInteractable>().attachTransform = fishTail.transform;
        }
    }
}
