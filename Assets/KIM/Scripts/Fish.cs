using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KIM
{
    public abstract class Fish : MonoBehaviour, IHittable
    {
        protected FishData data;

        protected abstract void Move();
        protected abstract void Die();
        public void Hit()
        {
           
        }
    }
}
