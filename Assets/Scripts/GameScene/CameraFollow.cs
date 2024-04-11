using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* CameraFollow.cs by bendux
 * https://www.youtube.com/watch?v=ZBj3LBA2vUY */

public class CameraFollow : MonoBehaviour
{
    private float smoothTime = 0.25f; // This controls how smooth the camera tracks the target.
    private Vector3 velocity = Vector3.zero; // This is the refered camera velocity.

    [Tooltip("This is where the camera stops tracking the pinball's y-position!")]
    public float minY = -10f; // Stop tracking if the pinball falls below this y-position!

    [SerializeField] private Transform target; // This is what the camera follows in the game scene.

    void FixedUpdate()
    {
        // If the camera's y-position is more than the minimum y-value...
        if(transform.position.y > minY)
        {
            Vector3 targetPosition = new Vector3(transform.position.x, target.position.y, -10f); // The offset refers to the 2D camera's required -10 z-axis.
            // Move camera to target position, from current position, smoothly by smoothTime.
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }       
    }
}
