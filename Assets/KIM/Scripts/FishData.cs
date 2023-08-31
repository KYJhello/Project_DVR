using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KIM
{
    enum FishType { NonAttackFish = 0, AttackFish }
    enum FishRank { Normal = 0, Rare, SuperRare, Special }
    public class FishData
    {
        private string name;
        private int hp;
        private float recognitionRange;
        private float moveSpeed;
        private float escapeSpeed;
        private float fishLength;
        private FishType curFishType;
        private FishRank curFishRank;

        public string Name { get { return name; } }
        public int HP { get { return hp; } }
        public float RecognitionRange { get { return recognitionRange; } }
        public float MoveSpeed { get { return moveSpeed; } }
        public float EscapeSpeed { get { return escapeSpeed; } }
        public float FishLength { get { return fishLength; } }
        //public enum CurFishType { get { return curFishType; } } 
        //public FishRank CurFishRank { get { return curFishRank} }

    }
}