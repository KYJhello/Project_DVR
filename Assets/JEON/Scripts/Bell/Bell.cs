using AHN;
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
    CustomerSqawnManager customerSqawn;
    Coroutine customerSpawnRoutine;

    private void Start()
    {
        timer = GameObject.Find("Clock").GetComponent<Timer>();
        customerSqawn = GameObject.Find("CustomerSpawnPoint").GetComponent<AHN.CustomerSqawnManager>();

        initialLocalPos = visualTarget.localPosition;

        interactable = GetComponent<XRBaseInteractable>();
        interactable.hoverEntered.AddListener(Follow);
        interactable.hoverExited.AddListener(ResetTarget);
        interactable.selectEntered.AddListener(Freeze);
    }

    public void Follow(BaseInteractionEventArgs hover)
    {
        if (hover.interactorObject is XRPokeInteractor) // ��ȣ�ۿ��ϴ� ��ü�� XRPokeInteractor�� ��쿡�� ����
        {
            XRPokeInteractor interactor = (XRPokeInteractor)hover.interactorObject;//XRPokeInteractor�� ����ȯ�Ͽ� ��ȣ�ۿ��ϴ� ��Ŀ�� ����� �����ɴϴ�.
 
            pokeAttechTransform = interactor.attachTransform; //pokeAttechTransform�� ��ȣ�ۿ��ϴ� ��Ŀ�� ����� ��ġ�� ȸ�� ������ �����մϴ�.

            // visualTarget�� ���� ��ġ�� pokeAttechTransform�� ��ġ ������ ����(������)�� ����մϴ�.
            // visualTarget�� ��Ŀ�� ����� ���󰡱� ���� ������� ��ġ�� ��Ÿ���ϴ�.
            offset = visualTarget.position - pokeAttechTransform.position;

            float pokeAngle = Vector3.Angle(offset, visualTarget.TransformDirection(localAxis));
            if (pokeAngle < followAngleTreshold)
            {
                isFollowing = true; //��Ŀ�� ����� ���󰡴� ������ �����մϴ�.
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
            if(customerSpawnRoutine == null)
            {
                StartCoroutine(customerSqawn.CustomerSpawnRoutine());   // �մ� ���� ����
            }

            MenuManager.StoreFishListInTankRoutine();   // �������� ����� ������ �޾ƿ��� ����
        }
    }

    private void Update()
    {
        if (freeze)
            return;

        if (isFollowing)
        {
            Vector3 localTargetPosition = visualTarget.InverseTransformPoint(pokeAttechTransform.position + offset); // ��Ŀ�� ����� ��ġ�� ȸ���� visualTarget�� ���� ��ǥ��� ��ȯ�մϴ�.

            // localTargetPosition�� localAxis ���� �������� �������� �����մϴ�.
            // �̷��� ���� ���ʹ� normal �������θ� ��ġ�� ���� ��Ÿ���ϴ�.
            Vector3 constrainedLocalTargetPosition = Vector3.Project(localTargetPosition, localAxis);

            // visualTarget�� ��ġ�� ��Ŀ�� ����� ��ġ�� offset�� ���� ������ �����մϴ�.
            // �̷ν� visualTarget�� �׻� ��Ŀ�� ����� ���󰡰� �˴ϴ�.
            visualTarget.position = visualTarget.TransformPoint(constrainedLocalTargetPosition);
        }
        else
        {
            visualTarget.localPosition = Vector3.Lerp(visualTarget.localPosition, initialLocalPos, resetSpeed * Time.deltaTime);
        }
    }

}
