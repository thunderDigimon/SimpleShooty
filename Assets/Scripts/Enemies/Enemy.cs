using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : AbstractEnemy
{
    protected override void OnKilledBy(GameObject go)
    {

    }

    protected override void SetUpEnemyConfig(GameObject go)
    {
        m_Speed = 2;
        m_Strength = 10;
        m_OnHitDeductVal = 3;
    }
}
