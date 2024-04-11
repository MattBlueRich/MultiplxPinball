using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* CameraFollow.cs by bendux
 * https://www.youtube.com/watch?v=ZBj3LBA2vUY */

public class CameraFollow : MonoBehaviour
{
    private Vector3 offset = new Vector3(0f, 0f, -10f);
    private float smoothTime = 0.25f;
    private Vector3 velocity = Vector3.zero;

    [Tooltip("This is where the camera stops tracking the pinball's y-position!")]
    public float minY = -10f;

    [SerializeField] private Transform target;

    // Update is called once per frame
    void Update()
    {
        // If the camera's y-position is more than the minimum y-value...
        if(transform.position.y > minY)
        {
            Vector3 targetPosition = target.position + offset;
            // Move camera to target position, from current position, smoothly by smoothTime.
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }       
    }
}
