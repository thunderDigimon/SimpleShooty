using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector3 m_Direction;
    private float m_BulletSpeed;

    public void onProjectileFinish()
    {
        Rigidbody bulletRigid = gameObject.GetComponent<Rigidbody>();
        bulletRigid.velocity = Vector3.zero; ;
        bulletRigid.angularVelocity = Vector3.zero;
        bulletRigid.angularDrag = 0;
        PoolManager.Instance.releaseObject(gameObject);
    }
}
