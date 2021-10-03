using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeSensor : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        Debug.LogError("Colliding with----" + collision.gameObject.name);
    }
}
