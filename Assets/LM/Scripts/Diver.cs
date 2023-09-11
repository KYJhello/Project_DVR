using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

namespace LM
{
    public class Diver : MonoBehaviour
    {
        [SerializeField] Canvas HUDCanvas;
        [SerializeField] GameObject helmet;
        
        public Material glassMat;
        public UnityEvent OnDived;
        public UnityEvent OnDiveEnded;
        public UnityEvent OnChangeWeight;
        public UnityEvent<float> OnChangedO2;
        
        public Device device;
        public Platform platform;

        DiverHelmetHUD HUD;
        XRSocketInteractor socketInteractor;
        Coroutine BreathRoutine;
        Vector3 prevPos;
        bool isDived;

        private bool invisible;
        private float invisibleTime;

        public float MaxO2 { get; set; }
        public float CurO2 { get; set; }
        public float MaxWeight { get; set; }
        public float CurWeight { get; set; }
        public float Depth { get; set; }

        private void Awake()
        {
            isDived = false;
            invisible = false;
            invisibleTime = 1;
            MaxO2 = 60;
            CurO2 = MaxO2;
            MaxWeight = 80;
            CurWeight = 0;
            Depth = 0;

            HUDCanvas.enabled = false;
            HUD = HUDCanvas.GetComponent<DiverHelmetHUD>();

            helmet.SetActive(false);
            HUD.enabled = false;

            socketInteractor = GetComponentInChildren<XRSocketInteractor>();
            
            if(platform != null)
                platform.gameObject.SetActive(false);
        }
        private void OnEnable()
        {
            socketInteractor.selectEntered.AddListener(HUDOn);
            socketInteractor.selectExited.AddListener(HUDOff);
        }
        private void OnDisable()
        {
            socketInteractor.selectEntered.RemoveListener(HUDOn);
            socketInteractor.selectExited.RemoveListener(HUDOff);
        }

        private void Update()
        {
            if (!socketInteractor.hasSelection)
                return;
            if (!isDived && prevPos.y > transform.position.y && transform.position.y < 0)
                OnDive();
            else if (isDived && prevPos.y < transform.position.y && transform.position.y > 0)
                OnDiveEnd();
            
            if(transform.position.y < 0)
                Depth = transform.position.y * 5;
            else
                Depth = 0;
        }

        private void FixedUpdate()
        {
            prevPos = transform.position;
        }



        private void OnDive()
        {
            if (!isDived)
            {
                OnDived?.Invoke();
                isDived = true;
                BreathRoutine = StartCoroutine(Breath());
                OnPlateEnable();
            }
        }
        private void OnDiveEnd()
        {
            if(isDived)
            {
                OnDiveEnded?.Invoke();
                isDived = false;
                StopCoroutine(BreathRoutine);
                CurO2 = MaxO2;
                OnPlateDisable();
            }
        }
        public void OnChangeO2(float o2)
        {
            if(o2 > 0)
            {
                OnChangedO2?.Invoke(o2);
                CurO2 += o2;
            }
            else
            {
                if (!invisible)
                {
                    StartCoroutine(InvisibleRoutine());
                    OnChangedO2?.Invoke(o2);
                    CurO2 += o2;
                }
            }
        }
        // ���ݹ����� ����� ��ȣ�ۿ�� ����Ʈ �ʿ�
        // -> �װ� o2change�� ����
        public void OnDied()
        {
            OnPlateDisable();
            // ������ �̵�
            // �κ� ����
            // �ʿ��Ѱ͵� �����Ű��
            // ���� �Ͻ��� ���� ���� �ʱ�ȭ
        }

        public void OnPlateEnable()
        {
            if (platform != null)
            {
                platform.transform.position = transform.position - new Vector3(0, 5f, 0);
                platform.gameObject.SetActive(true);
            }
            else
                platform = GameManager.Resource.Instantiate<Platform>("Platform", transform.position - new Vector3(0, 5f, 0), Quaternion.identity);
            device.platform = platform;
        }
        public void OnPlateDisable()
        {
            platform.gameObject.SetActive(false);
        }
        public void Escape()
        {

        }

        public void HUDOn(SelectEnterEventArgs args)
        {
            MeshRenderer[] renderers = args.interactableObject.transform.gameObject.GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer renderer in renderers)
            {
                renderer.enabled = false;
            }
            args.interactableObject.transform.GetComponent<BoxCollider>().size = new Vector3(0.5f, 1.5f, 1f);
            HUDCanvas.enabled = true;
            HUD.enabled = true;
            helmet.SetActive(true);
        }
        public void HUDOff(SelectExitEventArgs args)
        {
            MeshRenderer[] renderers = args.interactableObject.transform.gameObject.GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer renderer in renderers)
            {
                renderer.enabled = true;
            }
            args.interactableObject.transform.GetComponent<BoxCollider>().size = new Vector3(2, 2.5f, 2);
            HUDCanvas.enabled = false;
            HUD.enabled = false;
            helmet.SetActive(false);
        }
        IEnumerator InvisibleRoutine()
        {
            invisible = true;
            yield return new WaitForSeconds(invisibleTime);
            invisible = false;
        }
        IEnumerator Breath()
        {
            while(CurO2 <= MaxO2)
            {
                CurO2 -= Time.fixedDeltaTime;
                if (CurO2 < 0)
                {
                    OnDied();
                    CurO2 = 0;
                    StopAllCoroutines();
                }
                yield return new WaitForFixedUpdate();
            }
        }
    }
}