using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : Singleton<EnemyFactory>
{

    [SerializeField]
    private GameObject m_EnemyPrefab;


    void Awake()
    {
        PoolManager.Instance.warmPool(m_EnemyPrefab, 5);
    }
    public void SpawnEnemy(int inCount, Vector3 inSpawnRefPosition)
    {
        float radius = 4f;
        Vector3 refPos = new Vector3(0, 0.357f, inSpawnRefPosition.z);

        while (inCount-- > 0)
        {
            Vector3 spawnPos = (Random.insideUnitSphere * radius) + refPos;
            spawnPos.y = refPos.y;
            var randomRotation = Quaternion.Euler(0, Random.Range(0, 45), 0);
            GameObject enemy = PoolManager.Instance.spawnObject(m_EnemyPrefab, spawnPos, randomRotation);
        }
    }
}
