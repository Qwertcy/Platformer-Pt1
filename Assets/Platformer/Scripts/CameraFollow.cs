using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;   // Assign your player character here in the Inspector.
    public Vector3 offset = new Vector3(0f, 2f, -10f);

    // You can store the minimumX the camera is allowed to be at (optional).
    // For a strictly non-backtracking design, keep track of the camera's current max X.
    private float currentMaxX;

    private void Start()
    {
        // Initialize currentMaxX to the camera's starting X position (or target X).
        currentMaxX = transform.position.x;
    }

    void LateUpdate()
    {
        if (target != null)
        {
            // Calculate the desired camera position based on the player's position + offset
            float desiredX = target.position.x + offset.x;
            float desiredY = target.position.y + offset.y;
            float desiredZ = offset.z; // Typically -10 for 2D side-scrollers

            // Only update camera's X if the player has moved further to the right
            if (desiredX > currentMaxX)
            {
                currentMaxX = desiredX;
            }

            // Now use currentMaxX (the farthest right we've been) for the camera position
            transform.position = new Vector3(currentMaxX, desiredY, desiredZ);
        }
    }
}
