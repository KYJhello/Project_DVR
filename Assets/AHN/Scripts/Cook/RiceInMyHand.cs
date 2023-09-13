using SerializableCallback;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace AHN
{
    public class RiceInMyHand : MonoBehaviour
    {
        // RicePile에 손을 넣으면 Rice 한 블록이 손에 나오도록.
        [SerializeField] GameObject rice;

        private void Start()
        {
            rice = GameManager.Resource.Instantiate<GameObject>("SushiManager");
            rice.gameObject.transform.position = gameObject.transform.position;
        }
    }
}