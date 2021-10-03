using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeSensor : MonoBehaviour
{
    private Rigidbody m_RigidBody;
    private GameObject m_CurrentAimedObject;
    private PlayerController m_Player;

    public void Init(PlayerController inPlayer)
    {
        m_Player = inPlayer;
        m_RigidBody = GetComponent<Rigidbody>();
    }
    void OnCollisionEnter(Collision collision)
    {
        if (m_Player != null)
        {
            if (collision != null)
            {
                var dist = Vector3.Distance(collision.transform.position, transform.position);
                m_CurrentAimedObject = collision.gameObject;
            }
        }
    }

    public void CheckForAutoFire()
    {
        if (m_Player != null && m_CurrentAimedObject != null)
        {
            m_Player.FireTarget(m_CurrentAimedObject);
        }
    }

}
