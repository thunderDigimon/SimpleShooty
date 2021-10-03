using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractEnemy : MonoBehaviour
{
    private float m_Speed = 0.2f;
    protected float m_Strength = 50;
    private GameObject m_Player;
    bool m_Killed = false;

    void Awake()
    {
        m_Killed = false;
        m_Player = null;
    }

    public void OnPlayerOutRange(object obj)
    {
        m_Player = null;
    }

    public void OnPlayerInRange(object obj)
    {
        m_Player = obj as GameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject go = other.gameObject;
        if (go != null)
        {
            if (isBullet(go.tag))
            {
                OnBulletHit(go);
            }
        }
    }

    void OnBulletHit(GameObject go)
    {
        m_Strength -= 2;
        if (m_Strength <= 0)
        {
            onKilled(go);
        }

        go.GetComponent<Projectile>().onProjectileFinish();
    }

    void onKilled(GameObject go)
    {
        OnKilledBy(go);
        GameEventManager.Instance.TriggerEvent(GameEvent.ENEMY_KILLED, gameObject);
    }

    protected abstract void OnKilledBy(GameObject go);

    bool isBullet(string inTag)
    {
        return !string.IsNullOrEmpty(inTag) && inTag.CompareTo(Constants.kTagProjectile) == 0;
    }

    bool isPlayer(string inTag)
    {
        return !string.IsNullOrEmpty(inTag) && inTag.CompareTo(Constants.kTagPlayer) == 0;
    }

    public void FixedUpdate()
    {
        if (m_Killed)
            return;

        if (m_Player != null)
        {
            transform.LookAt(m_Player.transform);
            float distance = Vector3.Distance(transform.position, m_Player.transform.position);
            if (distance >= 0)
            {
                transform.position += transform.forward * m_Speed * Time.deltaTime;

                if (distance < 1)
                {
                    m_Killed = true;
                    GameEventManager.Instance.TriggerEvent(GameEvent.ENEMY_KILLED, gameObject);
                }
            }
        }
    }
}
