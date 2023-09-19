using KIM;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using UnityEngine.XR.Interaction.Toolkit;

public class Knife : MonoBehaviour
{
    private string fishRank;
    private string fishName;

    Transform fishPos;
    Transform fishBodyPos;
    Quaternion quaternion;

    GameObject headCuttingPoint;
    GameObject tailCuttingPoint;

    GameObject fishBody;

    GameObject fishHead;
    GameObject fishTail;

    float colliders;
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
        colliders = fishBody.GetComponentsInChildren<BoxCollider>().Length;
        fishBodyPos = other.transform;
        fishRank = other.GetComponent<StoreFish_Body>().FishRank;
        fishName = other.GetComponent<StoreFish_Body>().FishName;

        if (other.gameObject == headCuttingPoint)
        {
            HeadCutting();
        }
        if (other.gameObject == tailCuttingPoint)
        {
            TailCutting();
        }
        StartCoroutine(InstantiateFish(other));
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
        GameObject fishBodyMeat = GameManager.Resource.Instantiate<GameObject>("Jeon_Prefab/Fish_Body_Meat", fishPos.position, quaternion, false);
        fishBodyMeat.GetComponent<FishBodyMeat>().FishTier = fishRank;
        fishBodyMeat.GetComponent<FishBodyMeat>().FishName = fishName;


        Destroy(other.gameObject);
    }

    IEnumerator InstantiateFish(Collider other)
    {
        yield return new WaitForSeconds(0.01f);

        if (other.gameObject == fishBody && colliders == 3)
        {
            fishPos = fishBodyPos;
            GetRawFishPrefab(other);
        }

    }
}
