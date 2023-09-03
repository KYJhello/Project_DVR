using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace LM
{
    public abstract class HarpoonSpear : MonoBehaviour
    {
        public float maxRange = 5;

        protected int damage;
        protected Rigidbody rb;

        protected virtual void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }
        public abstract void OnFire(Vector3 dir, float force);
    }
}