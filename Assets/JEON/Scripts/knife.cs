using KIM;
using System.Collections;
using Unity.VisualScripting;
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

    int groomCount;
    bool isHeadCutting = false;
    bool isTailCutting = false;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"콜라이더 {other}가 들어옴");
        fishBodyPos = other.transform;
        if (other.gameObject.name == "HeadLine")
        {
            HeadCutting(other.gameObject);

        }
        else if (other.gameObject.name == "TailLine")
        {
            if (isHeadCutting)
                TailCutting(other.gameObject);
        }

        if (other.gameObject.name == "Fish")
        { 
            fishBody = other.gameObject;
            fishPos = other.transform;
            if (isHeadCutting && isTailCutting)
                groomCount++;

            Debug.Log($"{groomCount}");
            
            if (groomCount >= 3)
            {
                GetRawFishPrefab();
            }
        }
        /*fishRank = other.GetComponent<StoreFish_Body>().FishRank;
        fishName = other.GetComponent<StoreFish_Body>().FishName;*/
    }

    private void HeadCutting(GameObject go)
    {
        headCuttingPoint = go;
        fishHead = go.transform.GetChild(0).gameObject;

        Debug.Log("HeadCutting함수 실행");
        headCuttingPoint.GetComponent<BoxCollider>().enabled = false;
        fishHead.transform.SetParent(null);
        fishHead.GetComponent<BoxCollider>().enabled = true;
        fishHead.GetComponent<Rigidbody>().useGravity = true;
        fishHead.GetComponent<Rigidbody>().isKinematic = false;
        fishHead.GetComponent<XRGrabInteractable>().enabled = true;
        fishHead.GetComponent<XRGrabInteractable>().attachTransform = fishHead.transform;

        isHeadCutting = true;
    }

    private void TailCutting(GameObject go)
    {
        tailCuttingPoint = go;
        fishTail = go.transform.GetChild(0).gameObject;

        Debug.Log("TailCutting함수 실행");
        tailCuttingPoint.GetComponent<BoxCollider>().enabled = false;
        fishTail.transform.SetParent(null);
        fishTail.GetComponent<BoxCollider>().enabled = true;
        fishTail.GetComponent<Rigidbody>().useGravity = true;
        fishTail.GetComponent<Rigidbody>().isKinematic = false;
        fishTail.GetComponent<XRGrabInteractable>().enabled = true;
        fishTail.GetComponent<XRGrabInteractable>().attachTransform = fishTail.transform;

        isTailCutting = true;
    }

    private void GetRawFishPrefab()
    {
        quaternion = Quaternion.Euler(0, -90, 0);
        GameObject fishBodyMeat = GameManager.Resource.Instantiate<GameObject>("Jeon_Prefab/Fish_Body_Meat", fishPos.position, quaternion, false);
        /*fishBodyMeat.GetComponent<FishBodyMeat>().FishTier = fishRank;
        fishBodyMeat.GetComponent<FishBodyMeat>().FishName = fishName;*/

        Debug.Log($"없애기 {fishBody}");
        Destroy(fishBody);

        FishCuttingReset();

    }
    public void FishCuttingReset()
    {
        isHeadCutting = false;
        isTailCutting = false;
    }
}
