using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


namespace KIM
{
    public abstract class Fish : MonoBehaviour, IHittable
    {
        [SerializeField]
        protected FishData data;
        protected int curHp;
        protected float curLength;
        protected float curWeight;
        protected Vector3 moveDir;
        [SerializeField]
        protected Rigidbody rb;

        private void Awake()
        {
        }

        protected Vector3 GetRandVector()
        {
            return new Vector3(Random.Range(-1f, 1f), Random.Range(-0.1f, 0.1f), Random.Range(-1f, 1f));
        }
        
        protected bool WallDetect()
        {
            // 벽을 감지하면 true 없으면 false
            return (Physics.OverlapSphere(this.transform.position, data.WallRecognitionRange, 1 << 14) != null) ? true : false;
        }

        protected void Die()
        {

        }
        public void Hit()
        {
            curHp--;
        }

        private void SetHP()
        {
            curHp = data.HP;
        }
        private void SetLength()
        {
            float randomRangeValue = data.Length / 10 + data.Length % 10;
            curLength = data.Length + Random.Range(-randomRangeValue,randomRangeValue);
        }
        private void SetWeight()
        {
            float randomRangeValue = data.Weight / 10 + data.Weight % 10;
            curWeight = data.Weight;
        }

    }
}
