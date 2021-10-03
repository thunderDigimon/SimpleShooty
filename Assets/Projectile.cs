using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public void onProjectileFinish()
    {
        PoolManager.Instance.releaseObject(gameObject);
    }
}
