using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private AbstractWeapon m_CurrentWeapon;

    [SerializeField]
    private GameObject m_RangeSensorPrefab;

    [SerializeField]
    private float m_PlayerMoveSpeed;

    [SerializeField]
    private VariableJoystick m_JoystickController;

    [SerializeField]
    private CharacterController m_Player;

    [SerializeField]
    public float m_Gravity;

    private float mGroundDistance;
    private bool m_Moving;

    private RangeSensor m_RangeSensor;

    void Start()
    {
        mGroundDistance = m_Player.bounds.extents.y;
        SpawnRangeSensor();

        GameEventManager.Instance.UnregisterEventObserver(GameEvent.POINTER_STATUS, OnPointerStatus);
        GameEventManager.Instance.RegisterEventObserver(GameEvent.POINTER_STATUS, OnPointerStatus);
    }

    void OnPointerStatus(object inStatus)
    {
        m_Moving = (bool)inStatus;
    }

    void SpawnRangeSensor()
    {
        GameObject sensor = Instantiate(m_RangeSensorPrefab, Vector3.zero, Quaternion.identity, transform);
        m_RangeSensor = sensor.GetComponent<RangeSensor>();
        m_RangeSensor.Init(this);
        sensor.transform.SetAsLastSibling();
        sensor.transform.localPosition = Vector3.zero;
    }

    // Update is called once per frame
    public void FixedUpdate()
    {
        if (m_Moving)
        {
            Vector3 direction = Vector3.forward * m_JoystickController.Vertical + Vector3.right * m_JoystickController.Horizontal;
            direction.y = 0;
            transform.rotation = Quaternion.LookRotation(direction);
            direction.y = m_Gravity;
            m_Player.Move(direction * m_PlayerMoveSpeed * Time.deltaTime);
        }
        else
        {
            m_RangeSensor.CheckForAutoFire();
        }

        ApplyGravity();
    }

    void ApplyGravity()
    {
        // apply gravity effect
        if (!isGrounded())
        {
            m_Gravity += (Physics.gravity.y) * Time.deltaTime;
        }
        else
        {
            m_Gravity = 0f;
        }
    }

    bool isGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, mGroundDistance + 0.1f);
    }

    public void FireTarget(GameObject inTarget)
    {
        if (inTarget != null)
        {
            Vector3 direction = inTarget.transform.position - transform.position;
            direction.y = 0;
            transform.rotation = Quaternion.LookRotation(direction);
            m_CurrentWeapon.Fire(inTarget.transform);
        }
    }

    void OnDestroy()
    {
        GameEventManager.Instance.UnregisterEventObserver(GameEvent.POINTER_STATUS, OnPointerStatus);
    }

}
