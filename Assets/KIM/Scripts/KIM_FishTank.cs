using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KIM
{
    public class KIM_FishTank : MonoBehaviour
    {
        // fish list <fishInfo> 
        // fishInfo = name = 0, weight = 1, length = 2, FishRank = 3

        // 물고기 생성을 위한 물고기 리스트
        public List<List<string>> fishList = new List<List<string>>();

        // 통안에 들어있는 물고기 리스트
        public List<List<string>> totalFishList = new List<List<string>>();
        private bool isCreating = false;
        Coroutine createFishRoutine;

        private void OnTriggerEnter(Collider other)
        {
            // 만약 충돌한 물체의 레이어가 피쉬박스라면
            if(other.gameObject.layer == 15 && !isCreating)
            {
                //if(other.gameObject.GetComponent<FishBox>().GetFishList().Count <= 0) { return; }
                isCreating = true;
                //fishList = new List<List<string>>();
                List<List<string>> fishBoxFishList = new List<List<string>>();
                fishBoxFishList = other.gameObject.GetComponent<FishBox>().GetFishList();
                if(fishBoxFishList == null) { return; }
                createFishRoutine = StartCoroutine(CreateFishRoutine(fishBoxFishList));
            }
            //else if(other.gameObject.layer == 16)
            //{
            //    foreach()
            //}
        }
        private void OnTriggerExit(Collider other)
        {
            // 나간 물체의 레이어가 StoreFish고, 
            if(other.gameObject.layer == 16)
            {
                int index = 0;
                foreach(List<string> fishInfo in totalFishList)
                {
                    //물고기의 모든 정보가 같은경우
                    if(other.gameObject.GetComponent<StoreFishInfo>()?.FishName == fishInfo[0] &&
                        other.gameObject.GetComponent<StoreFishInfo>()?.Weight == fishInfo[1] &&
                        other.gameObject.GetComponent<StoreFishInfo>()?.Length == fishInfo[2] &&
                        other.gameObject.GetComponent<StoreFishInfo>()?.FishRank == fishInfo[3])
                    {
                        Debug.Log("TotalFishExit" + totalFishList.Count
                            + "\n fishList : " + fishList.Count);
                        totalFishList.RemoveAt(index);
                        return;
                    }
                    index++;
                }
            }
        }
        public List<List<string>> ReturnFishTankFishList()
        {
            return totalFishList;
        }
        public void AddFishTankFishList(List<List<string>> fishes)
        {
            isCreating = true;
            fishList = new List<List<string>>();
            StartCoroutine(CreateFishRoutine(fishes));
        }
        public void ClearTotalFishList()
        {
            totalFishList.Clear();
        }

        //IEnumerator CreateFishRoutine(Collider other)
        //{
        //    List<List<string>> fishBoxFishList = new List<List<string>>();
        //    fishBoxFishList = other.gameObject.GetComponent<FishBox>().GetFishList();
        //    foreach (List<string> fishInfo in fishBoxFishList)
        //    {
        //        fishList.Add(fishInfo);
        //        //totalFishList.Add(fishInfo);
        //    }
        //    foreach (List<string> fishInfo in fishList)
        //    {
        //        totalFishList.Add(fishInfo);
        //        GameManager.Resource.Instantiate<StoreFish>("Jeon_Prefab/Fish", transform.position + Vector3.up, Quaternion.identity).SetFishInfo(fishInfo);
        //        yield return new WaitForSeconds(0.2f);
        //    }
        //    isCreating = false;

        //    fishList.Clear();
        //    //StopAllCoroutines();
        //    yield return null;

        //}
        IEnumerator CreateFishRoutine(List<List<string>> fishes)
        {

            foreach (List<string> fishInfo in fishes)
            {
                fishList.Add(fishInfo);
                //totalFishList.Add(fishInfo);
            }
            foreach (List<string> fishInfo in fishList)
            {
                totalFishList.Add(fishInfo);
                if (fishInfo[0] == "Salmon")
                {
                    GameManager.Resource.Instantiate<StoreFishInfo>("Jeon_Prefab/Salmon", transform.position + Vector3.up, Quaternion.identity).SetFishInfo(fishInfo);

                }
                else if (fishInfo[0] == "Aji")
                {
                    GameManager.Resource.Instantiate<StoreFishInfo>("Jeon_Prefab/Aji", transform.position + Vector3.up, Quaternion.identity).SetFishInfo(fishInfo);

                }
                else if (fishInfo[0] == "Hirame")
                {
                    GameManager.Resource.Instantiate<StoreFishInfo>("Jeon_Prefab/Hirame", transform.position + Vector3.up, Quaternion.identity).SetFishInfo(fishInfo);

                }

                yield return new WaitForSeconds(0.2f);
            }
            isCreating = false;

            fishList.Clear();
            StopCoroutine(createFishRoutine);
            yield return null;

        }

    }
}