using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractWeapon : MonoBehaviour
{
    [SerializeField]
    private GameObject m_BulletPrefab;

    [SerializeField]
    private Transform m_BulletSpawnPos;
    private float m_Timer;
    protected float m_RecoilWaitTime = 5;
    protected float m_BulletSpeed = 20;
    protected WaitForSeconds m_MaxDistance = new WaitForSeconds(5);
    private bool m_Ready = false;

    void Awake()
    {
        m_BulletPrefab.SetActive(false);
        SpawnBulletsInPool();
        SetUpConfigTime();
        m_Ready = true;
    }

    void Update()
    {
        m_Timer += Time.deltaTime;

        if (m_Timer > m_RecoilWaitTime)
        {
            m_Ready = true;
            OnRecoiled();
            m_Timer = m_Timer - m_RecoilWaitTime;
        }
    }

    void SpawnBulletsInPool()
    {
        PoolManager.Instance.warmPool(m_BulletPrefab, 10);
    }

    protected GameObject GetBullet(Vector3 position, Quaternion rotation)
    {
        var bullet = PoolManager.SpawnObject(m_BulletPrefab, position, rotation);
        return bullet;
    }

    public virtual void Fire(Transform inTarget)
    {
        if (m_Ready)
        {
            m_Ready = false;
            GameObject bulletToFire = GetBullet(transform.position, Quaternion.identity);
            bulletToFire.SetActive(true);
            Rigidbody bulletRigid = bulletToFire.GetComponent<Rigidbody>();
            bulletRigid.position = m_BulletSpawnPos.position;
            Vector3 rotation = bulletToFire.transform.rotation.eulerAngles;
            bulletRigid.rotation = Quaternion.Euler(rotation.x, transform.eulerAngles.y, rotation.z);
            Vector3 direction = inTarget.position - transform.position;
            direction.y = 0;
            bulletRigid.AddForce(direction * m_BulletSpeed, ForceMode.Force);
            StartCoroutine(onMaxDistanceReached(bulletToFire));
        }
    }

    IEnumerator onMaxDistanceReached(GameObject inBullet)
    {
        yield return m_MaxDistance;
        inBullet.GetComponent<Projectile>().onProjectileFinish();
    }

    protected abstract void SetUpConfigTime();
    protected abstract void OnRecoiled();
}
