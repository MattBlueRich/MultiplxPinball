using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateTrigger : MonoBehaviour
{
    [Header("Gate Properties")]
    [Tooltip("Place empty GameObject's on each corner of the gate, then add them to this list!")]
    public Transform[] waypoints; // The waypoints are the points the ball moves between, and are children of this GameObject. 
    public float gateSpeed; // This is how fast the ball moves around the gate.
    public Vector2 endForce; // This is the direction and force added to the ball when it exits the gate.

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If the pinball enters into this GameObject's trigger...

        if (collision.gameObject.CompareTag("Pinball"))
        {
            // Start gate movement inside PinballScript.cs!
            collision.gameObject.GetComponent<PinballScript>().UseGateMovement(waypoints, gateSpeed, endForce);
        }
    }
}
