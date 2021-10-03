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
        m_BulletSpeed = 20;
        m_MaxDistance = new WaitForSeconds(5);
        m_RecoilWaitTime = 0.5f;
    }
}
