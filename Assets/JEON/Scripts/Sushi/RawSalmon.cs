using KIM;
using System.Collections.Generic;
using UnityEngine;
// 2��° �и�
public class RawSalmon : MonoBehaviour
{
    float cuttingCount; // ����� �ڸ��� Ƚ���� �����ϴ� ����

    GameObject knife; // Į ��ü�� �����ϴ� ����

    Material baseColor; // �⺻ ������ �����ϴ� ����
    MeshRenderer meshRenderer; // MeshRenderer ������Ʈ�� �����ϴ� ����

    public List<GameObject> fishMeats; // ����� ��⸦ �����ϴ� ����Ʈ
    private string fishTier; // ����� ����� �����ϴ� ����
    private string fishName; // ����� �̸��� �����ϴ� ����
    public string FishTier { get { return fishTier; } set { fishTier = value; } } // ����� ��� �Ӽ�
    public string FishName { get { return fishName; } set { fishName = value; } } // ����� �̸� �Ӽ�

    private void Awake()
    {
        knife = GameObject.Find("Knife"); // "Knife"�̸��� ���� ���� ������Ʈ�� ã�� knife ������ �Ҵ�
        meshRenderer = GetComponent<MeshRenderer>(); // �ڽ��� MeshRenderer ������Ʈ�� ������ meshRenderer ������ �Ҵ�

        baseColor = meshRenderer.material; // �⺻ ������ meshRenderer�� material���� ������ baseColor ������ �Ҵ�
        meshRenderer.material = baseColor; // meshRenderer�� material�� baseColor�� ����
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 25) // �浹�� ��ü�� ���̾ 25�� ���
        {
            Debug.Log("����"); // "����"�� �α׷� ���
            cuttingCount++; // �ڸ��� Ƚ�� ����
            if (cuttingCount >= 5) // �ڸ��� Ƚ���� 5 �̻��� ���
            {
                Debug.Log("��"); // "��"�� �α׷� ���

                foreach (GameObject salmon in fishMeats) // fishMeats ����Ʈ�� �ִ� ������ ����� ��⿡ ���� �ݺ�
                {
                    salmon.SetActive(true); // ����� ��⸦ Ȱ��ȭ
                    salmon.transform.SetParent(null); // �θ� ������ ����
                    salmon.GetComponent<RawFishForCutting>().FishTier = fishTier; // ����� ����� ����� ����
                    salmon.GetComponent<RawFishForCutting>().FishName = fishName; // ����� ����� �̸��� ����
                }
                gameObject.SetActive(false); // ���� ��ü ��Ȱ��ȭ
            }
        }
    }
}

