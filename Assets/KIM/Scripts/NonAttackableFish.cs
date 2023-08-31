using KIM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonAttackableFish : Fish
{
    protected override void Die()
    {
        throw new System.NotImplementedException();
    }

    protected override void Move()
    {
        throw new System.NotImplementedException();
    }
}
