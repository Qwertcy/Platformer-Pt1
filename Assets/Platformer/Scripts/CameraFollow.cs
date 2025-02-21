using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;   // Assign your player character here in the Inspector.
    public Vector3 offset = new Vector3(0f, 2f, -10f); // Adjust as needed.

    void LateUpdate()
    {
        if (target != null)
        {
            // Update the camera's position based on the target's position plus an offset.
            transform.position = target.position + offset;
        }
    }
}
