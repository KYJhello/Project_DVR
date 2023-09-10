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
        private float weight;
        [SerializeField]
        private float length;
        [SerializeField]
        private float playerRecognitionRange;
        [SerializeField]
        private float wallRecognitionRange;
        [SerializeField]
        private float moveSpeed;
        [SerializeField]
        private float escapeSpeed;
        [SerializeField]
        private bool isStopableFish;
        [SerializeField]
        private FishType curFishType;
        private FishRank curFishRank; 
        
        public FishType CurFishType { get { return curFishType; } set { curFishType = value; } }
        public FishRank CurFishRank { get { return curFishRank; } set { curFishRank = value; } }

        public string Name { get { return name; } }
        public int HP { get { return hp; } }
        public float Weight { get { return weight; } }
        public float Length { get { return length; } }

        public float PlayerRecognitionRange { get { return playerRecognitionRange; } }
        public float WallRecognitionRange { get { return wallRecognitionRange; } }

        public float MoveSpeed { get { return moveSpeed; } }
        public float EscapeSpeed { get { return escapeSpeed; } }
        public bool IsStopableFish { get { return isStopableFish; } }
    }
}