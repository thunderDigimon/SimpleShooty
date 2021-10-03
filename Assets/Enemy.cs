using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other != null && other.gameObject.tag.CompareTo("Projectile") == 0)
        {
            other.gameObject.GetComponent<Projectile>().onProjectileFinish();
        }
    }
}
