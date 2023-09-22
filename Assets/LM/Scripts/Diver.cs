using KIM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

namespace LM
{
    public class Diver : MonoBehaviour
    {
        [SerializeField] Canvas HUDCanvas;
        [SerializeField] GameObject helmet;
        [SerializeField] GameObject helmetLight;
        [SerializeField] ParticleSystem bubble;
        [SerializeField] Mask mask;
        
        public Material glassMat;
        public UnityEvent OnDived;
        public UnityEvent OnDiveEnded;
        public UnityEvent OnChangedWeight;
        public UnityEvent<float> OnChangedO2;
        
        public Device device;
        public Platform platform;
        public int level;

        FishBox box;
        ResetPos resetPos;
        DiverHelmetHUD HUD;
        XRSocketInteractor socketInteractor;
        CharacterController characterController;
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
            MaxO2 = 180;
            CurO2 = MaxO2;
            MaxWeight = 80;
            CurWeight = 0;
            Depth = 0;
            level = 0;

            box = FindObjectOfType<FishBox>();

            resetPos = GameManager.Resource.Load<ResetPos>("ResetPos");

            device = transform.parent.GetComponentInChildren<Device>();
            device.gameObject.SetActive(false);

            HUDCanvas.enabled = false;
            HUD = HUDCanvas.GetComponent<DiverHelmetHUD>();

            helmet.SetActive(false);
            HUD.enabled = false;

            socketInteractor = GetComponentInChildren<XRSocketInteractor>();

            characterController = transform.parent.parent.GetComponent<CharacterController>();
            
            if(platform != null)
                platform.gameObject.SetActive(false);

            helmetLight.SetActive(false);
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
            {
                if (!isDived && prevPos.y > transform.position.y && transform.position.y < 0)
                    OnDied();
                return;
            }
            if (!isDived && prevPos.y > transform.position.y && transform.position.y < 0)
                OnDive();
            else if (isDived && prevPos.y < transform.position.y && transform.position.y > 0)
                OnDiveEnd();
            
            if(transform.position.y < 0)
                Depth = transform.position.y * 2;
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
                GameManager.Sound.Play("Sounds/sfx_abyss", Define.Sound.Effect, 0.7f);
                bubble.Play();
                isDived = true;
                helmetLight.SetActive(true);
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
                helmetLight.SetActive(false);
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
        // 공격물고기 생기면 상호작용용 온히트 필요
        // -> 그걸 o2change로 연결
        public void OnChangeWeight()
        {
            OnChangedWeight?.Invoke();
        }
        public void OnDied()
        {
            mask.gameObject.SetActive(true);
            OnPlateDisable();
            OnDiveEnded?.Invoke();
            isDived = false;
            helmetLight.SetActive(false);
            StopCoroutine(BreathRoutine);
            CurO2 = MaxO2;
            characterController.Move(resetPos.PlayerInBoatPos);
            
            // 인벤 비우기
            // 필요한것들 재생시키기
            mask.gameObject.SetActive(false);
        }

        public void OnPlateEnable()
        {
            if (platform != null)
            {
                platform.transform.position = transform.position - new Vector3(0, 5f, 0);
                platform.gameObject.SetActive(true);
            }
            else
            {
                platform = GameManager.Resource.Instantiate<Platform>("Platform", transform.position - new Vector3(0, 5f, 0), Quaternion.identity);
                platform.gameObject.SetActive(true);
            }
                
            device.platform = platform;
            device.FindPlatform();
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
            // level = 
            switch(level)
            {
                case 0:
                    MaxO2 = 120;
                    CurO2 = MaxO2;
                    MaxWeight = 60;
                    break;
                case 1:
                    MaxO2 = 180;
                    CurO2 = MaxO2;
                    MaxWeight = 120;
                    break;
                case 2:
                    MaxO2 = 240;
                    CurO2 = MaxO2;
                    MaxWeight = 200;
                    break;
                default:
                    MaxO2 = 360;
                    CurO2 = MaxO2;
                    MaxWeight = 300;
                    break;
            }
            CurWeight = 0;
            foreach(List<string> list in box.fishList)
            {
                CurWeight += int.Parse(list[1]);
            }
            
            MeshRenderer[] renderers = args.interactableObject.transform.gameObject.GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer renderer in renderers)
            {
                renderer.enabled = false;
            }
            args.interactableObject.transform.GetComponent<BoxCollider>().size = new Vector3(0.5f, 1.5f, 1f);
            HUDCanvas.enabled = true;
            HUD.enabled = true;
            helmet.SetActive(true);
            device.gameObject.SetActive(true);
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
            device.gameObject.SetActive(false);
            if(transform.position.y < 0)
                OnDied();
        }
        IEnumerator InvisibleRoutine()
        {
            invisible = true;
            yield return new WaitForSeconds(invisibleTime);
            invisible = false;
        }
        IEnumerator Breath()
        {
            float count = 0;
            while(CurO2 <= MaxO2)
            {
                CurO2 -= Time.fixedDeltaTime;
                count += Time.fixedDeltaTime;
                if(count > 10)
                {
                    count = count % 10;
                    bubble.Play();
                    GameManager.Sound.Play("Sounds/msfx_abyss_portal");
                }
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