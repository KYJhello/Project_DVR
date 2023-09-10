using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

namespace KIM {
    public class KIM_TestTextBox : MonoBehaviour
    {
        [SerializeField] FishBox fishBox;
        [SerializeField] TMP_Text texts;

        private void Update()
        {
            texts.text = null;
            foreach(Dictionary<string,string> dic in fishBox.fishList)
            {
                var items = dic.Select(t => t.ToString());
                texts.text += string.Join(", ", items);
                texts.text += "\n";
            }
        }
    }
}