using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractEnemy : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameObject go = other.gameObject;
        if (go != null && isBullet(go.tag))
        {
            OnKilledBy(go);
            go.GetComponent<Projectile>().onProjectileFinish();
        }
    }

    protected abstract void OnKilledBy(GameObject go);

    bool isBullet(string inTag)
    {
        return !string.IsNullOrEmpty(inTag) && inTag.CompareTo(Constants.kTagProjectile) == 0;
    }

    public void FixedUpdate()
    {
        // if (m_Moving)
        // {
        //     Vector3 direction = inTarget.position - transform.position;
        //     direction.y = 0;
        //     Vector3 direction = Vector3.forward * m_JoystickController.Vertical + Vector3.right * m_JoystickController.Horizontal;
        //     transform.rotation = Quaternion.LookRotation(direction);
        //     m_Player.Move(direction * m_PlayerMoveSpeed * Time.deltaTime);
        // }
        // else
        // {
        //     m_RangeSensor.CheckForAutoFire();
        //     // }

        //     // ApplyGravity();
    }

}
