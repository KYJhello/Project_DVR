using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


namespace KIM
{
    public class Fish : MonoBehaviour, IHittable
    {
        List<string> fishInfo;

        [SerializeField]
        protected FishData data;
        protected int curHp;
        protected float curLength;
        protected float curWeight;
        protected Vector3 moveDir;
        protected Rigidbody rb;

        protected virtual void Awake()
        {
            rb = GetComponent<Rigidbody>();
            SetHP();
            SetLength();
            SetWeight();
        }

        protected Vector3 GetRandVector()
        {
            return new Vector3(Random.Range(-1f, 1f), Random.Range(-0.1f, 0.1f), Random.Range(-1f, 1f));
        }
        
        protected bool WallDetect()
        {
            // 벽을 감지하면 true 없으면 false
            return (Physics.OverlapSphere(this.transform.position, data.WallRecognitionRange, 1 << 14).Length >0);
        }
        
        protected void ChangeMoveDir()
        {
            Ray ray = new Ray();
            ray.origin = this.transform.position;
            ray.direction = this.transform.forward;
            RaycastHit hit;
            // 표면으로 레이케스트를 보내 RaycastHit을 구한다
            Physics.Raycast(ray, out hit, data.WallRecognitionRange, 1 << 14);

            // 벽 표면의 노말백터와 진행방향의 오른쪽노말백터를 내적하여
            // 값이 양수면 오른쪽, 음수면 왼쪽으로 진행
            //moveDir = Vector3.Dot(hit.normal, transform.right) >= 0 ?
            //    transform.right / 2 : -transform.right / 2;

            // 벽 표면의 노말벡터와 진행방향의 위쪽 벡터를 내적하여
            // 값이 양수면 위, 음수면 아래로 진행
            //moveDir += Vector3.Dot(hit.normal, transform.up) >= 0 ?
            //    transform.up / 2 : -transform.up / 2;

            moveDir = -moveDir;
        }

        // 애니메이션 끄고, 움직임 멈추고, grabinteractable 키기
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
        private void SetFishInfo()
        {
            fishInfo.Add(data.Name);
            fishInfo.Add(curWeight.ToString());
            fishInfo.Add(curLength.ToString());
            //enum
            fishInfo.Add(data.curFishType.ToString());
            //enum
            fishInfo.Add(data.curFishRank.ToString());
        }
    }
}
