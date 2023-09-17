using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AHN
{
    public class StateCorutineManager : MonoBehaviour
    {
        // StateMachine에서는 Corutine이 안 되니까 여기서 코루틴 함수들 다 구현하고
        // State애니메이션 쪽에서 animator.GetComponent<StateCorutineManager>로 가져와서 사용 ㄱㄱ

        Animator anim;

        private void Awake()
        {
            anim = GetComponent<Animator>();
        }

        public IEnumerator OrderingRoutine()    // 주문 코루틴
        {
            anim.SetTrigger("IsFrontKiosk");
            yield return new WaitForSeconds(3f);
            anim.SetTrigger("FinishedOrdering");
        }

        public IEnumerator FoodWaitRoutine()    // 음식 기다리는 코루틴
        {
            yield return new WaitForSeconds(10f);
            anim.SetTrigger("Angry");
            yield return new WaitForSeconds(4f);
            anim.SetTrigger("GoOut");
            // TODO : 여기서 게임시간을 감소시켜야 할듯 (15초 정도)
            // 손님이 게임시간을 참조해서 그 시간을 차감하도록
        }

        public IEnumerator EatRoutine()     // 먹는 코루틴
        {
            yield return new WaitForSeconds(7f);
            anim.SetTrigger("GoOut");
        }
    }
}