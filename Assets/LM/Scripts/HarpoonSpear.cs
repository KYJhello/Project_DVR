using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace LM
{
    public abstract class HarpoonSpear : MonoBehaviour
    {
        public float maxRange;

        public int damage;

        public int Damage { get { return damage; } protected set { damage = value; } }
        public abstract void OnFire(Vector3 dir, float force);
    }
}