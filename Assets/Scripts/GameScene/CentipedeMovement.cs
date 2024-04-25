using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentipedeMovement : MonoBehaviour
{
    public Transform[] waypoints;
    private int waypointCount;
    private bool isMoving = true;
    private int nextPoint = 0;
    public float moveSpeed = 5;
    public GameObject[] targets;
    private Quaternion targetRotation;
    public bool isRotating = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            // If the ball hasn't reached the last point on the gate...
            if (nextPoint < waypoints.Length)
            {
                // If the ball hasn't reached its current point...
                if (transform.position != waypoints[nextPoint].position)
                {
                    // Move to the next point on the gate.
                    transform.position = Vector2.MoveTowards(transform.position,
                        waypoints[nextPoint].position, moveSpeed * Time.deltaTime);                   
                }
                // Else if the ball has reached its current point...
                else
                {
                    // Move to the next point on the gate.
                    nextPoint++;

                    transform.Rotate(0, 0, -90);
                    FixTargetRotation();
                }
            }
            // Else if the ball has finished riding the gate...
            else
            {
                nextPoint = 0;
            }
        }
    }

    void FixTargetRotation()
    {
        for (int i = 0; i < targets.Length; i++)
        {
            targets[i].transform.Rotate(0, 0, -270);
        }
    }

}
