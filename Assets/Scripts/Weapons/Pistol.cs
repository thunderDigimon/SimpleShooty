using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : AbstractWeapon
{
    protected override void OnRecoiled()
    {

    }

    protected override void SetUpWeaponConfig()
    {
        m_BulletSpeed = 70;
        m_MaxDistance = new WaitForSeconds(2);
        m_RecoilWaitTime = 0.75f;
    }
}
