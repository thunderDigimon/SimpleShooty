using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    public float horizontalFoV = 70.0f;

    void FixedUpdate()
    {
        if (Target != null)
        {
            Vector3 desiredPosition = Target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }

}
