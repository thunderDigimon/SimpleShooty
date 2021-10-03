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
        PoolManager.Instance.warmPool(m_BulletPrefab, 50);
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
            Debug.Log(m_BulletSpawnPos);
            Rigidbody bulletRigid = bulletToFire.GetComponent<Rigidbody>();
            bulletRigid.position = m_BulletSpawnPos.position;
            Vector3 rotation = bulletToFire.transform.rotation.eulerAngles;
            bulletRigid.rotation = Quaternion.Euler(rotation.x, transform.eulerAngles.y, rotation.z);
            Vector3 direction = inTarget.position - transform.position;
            bulletRigid.AddForce(direction * m_BulletSpeed, ForceMode.Impulse);
            StartCoroutine(onMaxDistanceReached(bulletToFire));
        }
    }

    IEnumerator onMaxDistanceReached(GameObject inBullet)
    {
        yield return m_MaxDistance;
        PoolManager.Instance.releaseObject(inBullet);
    }

    protected abstract void SetUpConfigTime();
    protected abstract void OnRecoiled();
}
