using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeSensor : MonoBehaviour
{
    private Rigidbody m_RigidBody;
    private GameObject m_CurrentAimedObject;
    private PlayerController m_Player;
    private List<GameObject> m_EnemiesInRange;

    public void Init(PlayerController inPlayer)
    {
        m_Player = inPlayer;
        m_RigidBody = GetComponent<Rigidbody>();
        m_EnemiesInRange = new List<GameObject>();
    }
    void OnCollisionEnter(Collision collision)
    {
        if (m_Player != null)
        {
            if (collision != null && isEnemy(collision.gameObject))
            {
                if (!m_EnemiesInRange.Contains(collision.gameObject))
                {
                    m_EnemiesInRange.Add(collision.gameObject);
                    var dist = Vector3.Distance(collision.transform.position, transform.position);
                }
            }
        }
    }

    bool isEnemy(GameObject inGo)
    {
        return inGo != null && inGo.tag.CompareTo(Constants.kTagEnemy) == 0;
    }

    void OnCollisionExit(Collision other)
    {
        if (m_CurrentAimedObject == other.gameObject)
            m_CurrentAimedObject = null;

        if (other != null && isEnemy(other.gameObject))
        {
            print("No longer in contact with " + other.transform.name);
            if (m_EnemiesInRange.Contains(other.gameObject))
            {
                m_EnemiesInRange.Remove(other.gameObject);
            }
        }
    }

    void Update()
    {
        if (m_Player == null)
            return;

        if (m_CurrentAimedObject == null)
        {
            //TODO: based on nearest distance
            m_CurrentAimedObject = m_EnemiesInRange.Count > 0 ? m_EnemiesInRange[0] : null;
        }

        transform.position = m_Player.transform.position;
    }

    public void CheckForAutoFire()
    {
        if (m_Player != null && m_CurrentAimedObject != null)
        {
            m_Player.FireTarget(m_CurrentAimedObject);
        }
    }

}
