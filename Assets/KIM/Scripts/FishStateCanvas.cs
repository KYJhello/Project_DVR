using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace KIM
{
    public class FishStateCanvas : MonoBehaviour
    {
        [SerializeField] NonAttackableFish fish;
        [SerializeField] TMP_Text test;

        private void Update()
        {
            test.text = fish.GetCurState();
            test.text += "\n " + "curHP : " + fish.CurHp;
        }
    }
}