using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : AbstractWeapon
{

    protected override void OnRecoiled()
    {

    }

    protected override void SetUpConfigTime()
    {
        m_BulletSpeed = 50;
        m_MaxDistance = new WaitForSeconds(5);
        m_RecoilWaitTime = 1f;
    }
}
