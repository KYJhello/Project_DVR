using KIM;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Jeon
{
    public class StoreFish : MonoBehaviour
    {
        private static string fishTier;
        private static string fishName;
        public string FishTier { get { return fishTier; } set { fishTier = value; } }
        public string FishName { get { return fishName; } set { fishName = value; } }

        protected BoxCollider[] fishColliders;

        protected BoxCollider headLine;
        protected BoxCollider tailLine;

        protected GameObject fishHead;
        protected GameObject fishTail;
        protected GameObject fishBody;

        Quaternion quaternion;
        Transform fishPos;

        StoreFish_Body body;

        protected void Awake()
        {
            fishColliders = gameObject.GetComponentsInChildren<BoxCollider>();
            body = fishColliders[1].gameObject.GetComponent<StoreFish_Body>();
        }
        protected void Start()
        {
            headLine = fishColliders[2];
            tailLine = fishColliders[4];

            fishHead = headLine.transform.GetChild(0).gameObject;
            fishTail = tailLine.transform.GetChild(0).gameObject;
            fishBody = fishColliders[1].gameObject;
            fishPos = fishColliders[1].gameObject.transform;
        }

        public void HeadCutting()
        {
            body.isHeadCutting = true;

            gameObject.GetComponent<BoxCollider>().enabled = false;
            fishHead.transform.SetParent(null);
            fishHead.GetComponent<BoxCollider>().enabled = true;
            fishHead.GetComponent<Rigidbody>().useGravity = true;
            fishHead.GetComponent<Rigidbody>().isKinematic = false;
            fishHead.GetComponent<XRGrabInteractable>().enabled = true;
            fishHead.GetComponent<XRGrabInteractable>().attachTransform = fishHead.transform;

            fishBody.transform.SetParent(null);
            fishBody.GetComponent<XRGrabInteractable>().enabled = true;
            fishBody.GetComponent<Rigidbody>().isKinematic = false;
            fishBody.GetComponent<BoxCollider>().enabled = true;
            fishBody.GetComponent<BoxCollider>().isTrigger = false;
        }
        public void TailCutting()
        {
            body.isTailCutting = true;

            gameObject.GetComponent<BoxCollider>().enabled = false;
            fishTail.transform.SetParent(null);
            fishTail.GetComponent<BoxCollider>().enabled = true;
            fishTail.GetComponent<Rigidbody>().useGravity = true;
            fishTail.GetComponent<Rigidbody>().isKinematic = false;
            fishTail.GetComponent<XRGrabInteractable>().enabled = true;
            fishTail.GetComponent<XRGrabInteractable>().attachTransform = fishTail.transform;

            fishBody.transform.SetParent(null);
            fishBody.GetComponent<XRGrabInteractable>().enabled = true;
            fishBody.GetComponent<Rigidbody>().isKinematic = false;
            fishBody.GetComponent<BoxCollider>().enabled = true;
            fishBody.GetComponent<BoxCollider>().isTrigger = false;
        }
    }
}
