using KIM;
using System.Collections.Generic;
using UnityEngine;
// 2번째 분리
public class RawSalmon : MonoBehaviour
{
    float cuttingCount; // 물고기 자르기 횟수를 저장하는 변수

    GameObject knife; // 칼 객체를 저장하는 변수

    Material baseColor; // 기본 색상을 저장하는 변수
    MeshRenderer meshRenderer; // MeshRenderer 컴포넌트를 저장하는 변수

    public List<GameObject> fishMeats; // 물고기 고기를 저장하는 리스트
    private string fishTier; // 물고기 등급을 저장하는 변수
    private string fishName; // 물고기 이름을 저장하는 변수
    public string FishTier { get { return fishTier; } set { fishTier = value; } } // 물고기 등급 속성
    public string FishName { get { return fishName; } set { fishName = value; } } // 물고기 이름 속성

    private void Awake()
    {
        knife = GameObject.Find("Knife"); // "Knife"이름을 가진 게임 오브젝트를 찾아 knife 변수에 할당
        meshRenderer = GetComponent<MeshRenderer>(); // 자신의 MeshRenderer 컴포넌트를 가져와 meshRenderer 변수에 할당

        baseColor = meshRenderer.material; // 기본 색상을 meshRenderer의 material에서 가져와 baseColor 변수에 할당
        meshRenderer.material = baseColor; // meshRenderer의 material을 baseColor로 설정
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 25) // 충돌한 객체의 레이어가 25인 경우
        {
            Debug.Log("들어옴"); // "들어옴"을 로그로 출력
            cuttingCount++; // 자르기 횟수 증가
            if (cuttingCount >= 5) // 자르기 횟수가 5 이상인 경우
            {
                Debug.Log("컷"); // "컷"을 로그로 출력

                foreach (GameObject salmon in fishMeats) // fishMeats 리스트에 있는 각각의 물고기 고기에 대해 반복
                {
                    salmon.SetActive(true); // 물고기 고기를 활성화
                    salmon.transform.SetParent(null); // 부모 설정을 해제
                    salmon.GetComponent<RawFishForCutting>().FishTier = fishTier; // 물고기 고기의 등급을 설정
                    salmon.GetComponent<RawFishForCutting>().FishName = fishName; // 물고기 고기의 이름을 설정
                }
                gameObject.SetActive(false); // 현재 객체 비활성화
            }
        }
    }
}

