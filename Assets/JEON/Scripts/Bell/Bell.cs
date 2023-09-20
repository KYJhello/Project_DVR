using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Bell : MonoBehaviour
{
    public Transform visualTarget;
    public Vector3 localAxis;
    public float resetSpeed = 5;
    public float followAngleTreshold;

    private Vector3 initialLocalPos;

    private Vector3 offset;
    private Transform pokeAttechTransform;

    private XRBaseInteractable interactable;

    private bool isFollowing = false;
    private bool freeze = false;

    private Timer timer;
    [SerializeField] GameObject customerSpawn;

    private void Start()
    {
        timer = GameObject.Find("Clock").GetComponent<Timer>();
        //customerSqawn = GameObject.Find("CustomerSpawnPoint").GetComponent<AHN.CustomerSqawnManager>();

        initialLocalPos = visualTarget.localPosition;

        interactable = GetComponent<XRBaseInteractable>();
        interactable.hoverEntered.AddListener(Follow);
        interactable.hoverExited.AddListener(ResetTarget);
        interactable.selectEntered.AddListener(Freeze);
    }

    public void Follow(BaseInteractionEventArgs hover)
    {
        if (hover.interactorObject is XRPokeInteractor) // 상호작용하는 객체가 XRPokeInteractor인 경우에만 실행
        {
            XRPokeInteractor interactor = (XRPokeInteractor)hover.interactorObject;//XRPokeInteractor로 형변환하여 상호작용하는 포커스 대상을 가져옵니다.
 
            pokeAttechTransform = interactor.attachTransform; //pokeAttechTransform에 상호작용하는 포커스 대상의 위치와 회전 정보를 저장합니다.

            // visualTarget의 현재 위치와 pokeAttechTransform의 위치 사이의 차이(오프셋)를 계산합니다.
            // visualTarget이 포커스 대상을 따라가기 위한 상대적인 위치를 나타냅니다.
            offset = visualTarget.position - pokeAttechTransform.position;

            float pokeAngle = Vector3.Angle(offset, visualTarget.TransformDirection(localAxis));
            if (pokeAngle < followAngleTreshold)
            {
                isFollowing = true; //포커스 대상을 따라가는 동작을 시작합니다.
                freeze = false;
            }
        }
    }

    public void ResetTarget(BaseInteractionEventArgs hover)
    {
        if (hover.interactorObject is XRPokeInteractor)
        {
            isFollowing = false;
            freeze = false;
        }
    }

    public void Freeze(BaseInteractionEventArgs hover)
    {
        if (hover.interactorObject is XRPokeInteractor)
        {
            freeze = true;
            timer.StertSell();
            //customerSqawn.CustomerSpawnRoutine();   // 손님 생성
            customerSpawn.gameObject.SetActive(true);
            MenuManager.StoreFishListInTankRoutine();
        }
    }

    private void Update()
    {
        if (freeze)
            return;

        if (isFollowing)
        {
            Vector3 localTargetPosition = visualTarget.InverseTransformPoint(pokeAttechTransform.position + offset); // 포커스 대상의 위치와 회전을 visualTarget의 로컬 좌표계로 변환합니다.

            // localTargetPosition을 localAxis 벡터 방향으로 수직으로 투영합니다.
            // 이렇게 얻은 벡터는 normal 방향으로만 미치는 힘을 나타냅니다.
            Vector3 constrainedLocalTargetPosition = Vector3.Project(localTargetPosition, localAxis);

            // visualTarget의 위치를 포커스 대상의 위치에 offset을 더한 값으로 설정합니다.
            // 이로써 visualTarget은 항상 포커스 대상을 따라가게 됩니다.
            visualTarget.position = visualTarget.TransformPoint(constrainedLocalTargetPosition);
        }
        else
        {
            visualTarget.localPosition = Vector3.Lerp(visualTarget.localPosition, initialLocalPos, resetSpeed * Time.deltaTime);
        }
    }

}
