using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KIM
{
    public class Shark : AttackableFish
    {
        protected override void Awake()
        {
            base.Awake();
            attackDamage = 30f;
            attackCoolTime = 10f;
        }
    }
}