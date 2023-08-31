using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KIM
{
    public abstract class Fish : MonoBehaviour, IHittable
    {
        [SerializeField]
        protected FishData data;
        protected int curHp;

        private void Awake()
        {
            curHp = data.HP;
        }

        protected abstract void Move();
        protected abstract void Die();
        public void Hit()
        {
            curHp--;
        }
    }
}
