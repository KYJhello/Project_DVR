using KIM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using UnityEngine.XR.Interaction.Toolkit;

public class Knife : MonoBehaviour
{
    Transform fishPos;
    Quaternion quaternion;

    GameObject headCuttingPoint;
    GameObject tailCuttingPoint;

    GameObject fishBody;

    GameObject fishHead;
    GameObject fishTail;
    private void Awake()
    {
        headCuttingPoint = GameObject.Find("HeadLine");
        tailCuttingPoint = GameObject.Find("TailLine");

        fishBody = GameObject.Find("FishBody");

        fishHead = GameObject.Find("CuttingPoint_1");
        fishTail = GameObject.Find("CuttingPoint_2");
    }

    private void OnTriggerEnter(Collider other)
    {
        float colliders = fishBody.GetComponentsInChildren<BoxCollider>().Length;
        Transform fishBodyPos = other.transform;

        if (other.gameObject == headCuttingPoint)
        {
            HeadCutting();
        }
        if (other.gameObject == tailCuttingPoint)
        {
            TailCutting();
        }
        if (other.gameObject == fishBody && colliders == 3)
        {
            fishPos = fishBodyPos;
            GetRawFishPrefab(other);
        }
    }

    private void HeadCutting()
    {
        headCuttingPoint.GetComponent<BoxCollider>().enabled = false;
        fishHead.transform.SetParent(null);
        fishHead.GetComponent<BoxCollider>().enabled = true;
        fishHead.GetComponent<Rigidbody>().useGravity = true;
        fishHead.GetComponent<Rigidbody>().isKinematic = false;
        fishHead.GetComponent<XRGrabInteractable>().enabled = true;
        fishHead.GetComponent<XRGrabInteractable>().attachTransform = fishHead.transform;
    }

    private void TailCutting()
    {
        tailCuttingPoint.GetComponent<BoxCollider>().enabled = false;
        fishTail.transform.SetParent(null);
        fishTail.GetComponent<BoxCollider>().enabled = true;
        fishTail.GetComponent<Rigidbody>().useGravity = true;
        fishTail.GetComponent<Rigidbody>().isKinematic = false;
        fishTail.GetComponent<XRGrabInteractable>().enabled = true;
        fishTail.GetComponent<XRGrabInteractable>().attachTransform = fishTail.transform;
    }

    private void GetRawFishPrefab(Collider other)
    {
        quaternion = Quaternion.Euler(0, -90, 0);
        GameManager.Resource.Instantiate<GameObject>("Jeon_Prefab/Fish_Body_Meat", fishPos.position, quaternion, false);
        Destroy(other.gameObject);
    }
}
