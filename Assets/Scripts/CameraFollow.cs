using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform target;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    public float horizontalFoV = 70.0f;

    void Start()
    {
        UpdateFOV();
    }

    void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }

    void UpdateFOV()
    {
        float halfWidth = Mathf.Tan(0.5f * horizontalFoV * Mathf.Deg2Rad);
        float halfHeight = halfWidth * Screen.height / Screen.width;
        float verticalFoV = 2.0f * Mathf.Atan(halfHeight) * Mathf.Rad2Deg;
        Camera.main.fieldOfView = verticalFoV;
    }

}
