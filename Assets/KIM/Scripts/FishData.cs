using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KIM
{
    public enum FishType { NonAttackFish = 0, AttackFish }
    public enum FishRank { Normal = 0, Rare, SuperRare, Special }

    [CreateAssetMenu(fileName = "Fish Data", menuName = "Scriptable Object/Fish Data", order =int.MaxValue)]
    public class FishData : ScriptableObject
    {
        [SerializeField]
        private new string name;
        [SerializeField]
        private int hp;
        [SerializeField]
        private float recognitionRange;
        [SerializeField]
        private float moveSpeed;
        [SerializeField]
        private float escapeSpeed;
        private float fishLength;
        public FishType curFishType { get; set; }
        public FishRank curFishRank { get; set; }

        public string Name { get { return name; } }
        public int HP { get { return hp; } }
        public float RecognitionRange { get { return recognitionRange; } }
        public float MoveSpeed { get { return moveSpeed; } }
        public float EscapeSpeed { get { return escapeSpeed; } }
        public float FishLength { get { return fishLength; } set { fishLength = value; } }

    }
}