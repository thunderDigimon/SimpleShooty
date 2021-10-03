using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private GameObject m_RangeSensor;

    [SerializeField]
    private float m_PlayerMoveSpeed;

    [SerializeField]
    private VariableJoystick m_JoystickController;

    [SerializeField]
    private CharacterController m_Player;

    [SerializeField]
    public float m_Gravity;

    private float mGroundDistance;

    void Start()
    {
        mGroundDistance = m_Player.bounds.extents.y;
        SpawnRangeSensor();
    }

    void SpawnRangeSensor()
    {
        GameObject sensor = Instantiate(m_RangeSensor, Vector3.zero, Quaternion.identity, transform);
        sensor.transform.SetAsLastSibling();
    }

    // Update is called once per frame
    public void FixedUpdate()
    {
        Vector3 direction = Vector3.forward * m_JoystickController.Vertical + Vector3.right * m_JoystickController.Horizontal;
        transform.rotation = Quaternion.LookRotation(direction);
        direction.y = m_Gravity;
        m_Player.Move(direction * m_PlayerMoveSpeed * Time.deltaTime);

        ApplyGravity();
    }

    void ApplyGravity()
    {
        // apply gravity effect
        if (!isGrounded())
        {
            Debug.Log("I am NOT grounded. Gravity = " + m_Gravity.ToString());
            m_Gravity += (Physics.gravity.y) * Time.deltaTime;
        }
        else
        {
            m_Gravity = 0f;
            Debug.Log("Grounded. Gravity = " + m_Gravity.ToString());
        }
    }

    bool isGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, mGroundDistance + 0.1f);
    }

}
