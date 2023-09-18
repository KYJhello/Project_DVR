using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

namespace KIM
{
    public enum FishRank { Normal = 0, Rare, SuperRare, Special }
    public abstract class Fish : MonoBehaviour, IHittable
    {
        // 이름, 무게, 길이, 랭크
        List<string> fishInfo = new List<string>();

        // 물고기 랭크
        private FishRank curFishRank;
        public FishRank CurFishRank { get { return curFishRank; } set { curFishRank = value; } }

        [SerializeField]
        protected FishData data;
        protected int curHp;
        protected float curLength;
        protected float curWeight;
        protected Vector3 moveDir;
        protected Rigidbody rb;
        protected bool isDie = false;
        protected bool isDirChangeActive = false;
        protected XRGrabInteractable grabInteractable;
        protected int curHitDamage;
        protected bool isHittable;

        public int CurHp { get { return curHp; } }

        protected virtual void Awake()
        {
            rb = GetComponent<Rigidbody>();
            grabInteractable = GetComponent<XRGrabInteractable>();
            grabInteractable.enabled = false;

            SetFishInfo();
            
        }

        protected Vector3 GetRandVector()
        {
            return new Vector3(Random.Range(-1f, 1f), Random.Range(-0.3f, 0.3f), Random.Range(-1f, 1f));
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

            if (isDirChangeActive == false)
            {
                isDirChangeActive = true;
                StartCoroutine(ChangeDirRoutine());
            }
            
        }
        IEnumerator ChangeDirRoutine()
        {
            while (isDirChangeActive)
            {
                //rb.rotation = Quaternion.LookRotation(transform.forward);
                moveDir = -moveDir.normalized;
                yield return new WaitForSeconds(1f);

                isDirChangeActive = false;
            }
        }

        // 애니메이션 끄고, 움직임 멈추고, grabinteractable 키기
        protected void Die()
        {
            isDie = true;
            StopAllCoroutines();
            grabInteractable.enabled = true;
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
            float randomRangeValue = data.Length / 10;
            curLength = data.Length + Random.Range(-randomRangeValue,randomRangeValue);
            curLength = Mathf.Floor(curLength * 100f) / 100f;
        }
        private void SetWeight()
        {
            float randomRangeValue = data.Weight / 10;
            curWeight = data.Weight + Random.Range(-randomRangeValue, randomRangeValue);
            curWeight = Mathf.Floor(curWeight * 100f) / 100f;
        }
        // 확률
        // 스페셜 3%
        // 슈퍼레어 10%
        // 레어 27%
        // 노말 60%
        private void SetFishRank()
        {
            int num = Random.Range(1, 100);
            if (num >=1 && num <=3)
            {
                CurFishRank = FishRank.Special;
            }
            else if (num > 3 && num <= 13)
            {
                CurFishRank = FishRank.SuperRare;
            }else if (num > 13 && num <= 40)
            {
                CurFishRank = FishRank.Rare;
            }else if(num > 40 && num <= 100)
            {
                CurFishRank = FishRank.Normal;
            }
        }
        private void SetFishInfo()
        {
            SetHP();
            SetLength();
            SetWeight();
            SetFishRank();

            fishInfo.Add(data.Name);
            fishInfo.Add(curWeight.ToString());
            fishInfo.Add(curLength.ToString());
            //enum to string
            fishInfo.Add(CurFishRank.ToString());
        }
        public bool GetIsDie()
        {
            return isDie;
        }
        public List<string> GetFishInfo()
        {
            StartCoroutine(DestroyRoutine());
            return fishInfo;
        }
        public List<string> GetJustFishInfo()
        {
            return fishInfo;
        }
        IEnumerator DestroyRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(0.05f);
                Destroy(this.gameObject);
            }
        }
        public abstract string GetCurState();
    }
}
