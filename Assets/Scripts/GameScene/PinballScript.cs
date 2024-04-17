using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PinballScript : MonoBehaviour
{
    // Animator Variables
    private Animator animator;
    private int verticalHash;
    private Rigidbody2D rb;

    // Pinball Game Over Variables
    [Header("Pinball Game Over")]
    public float deathHeight = -13f;
    public AudioClip deathSFX;
    public UnityEvent UEOutGameOver;
    private AudioSource audioSource;
    private bool gameOver = false;

    // Gate Variables
    private Transform[] gateWaypoints;
    [HideInInspector] public bool usingGate = false;
    private int nextPoint = 0;
    private float gateSpeed;
    private Vector2 gateEndForce;
    private CircleCollider2D circleCollider;

    void Start()
    {
        // StringToHash() saves memory on the Animator's parameters.
        verticalHash = Animator.StringToHash("PinballY");

        // Get access to components:
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        circleCollider = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        #region Pinball Animation
        // This fetches the pinball's RigidBody velocity, and normalizes the values (-1, 0 or 1).
        Vector2 pinballVelocity = new Vector2(rb.velocity.x, rb.velocity.y).normalized;
        float pinballYVelocity = pinballVelocity.y;

        // PinballY parameter is set to the pinball's y-velocity.
        animator.SetFloat(verticalHash, pinballYVelocity);
        #endregion

        #region Outlanes Game Over Detection
        if (transform.position.y <= deathHeight && !gameOver)
        {
            gameOver = true;

            UEOutGameOver.Invoke(); // Trigger public game over event.

            audioSource.PlayOneShot(deathSFX);
        }
        #endregion

        #region Gate Mechanic
        // If the ball enters the gate...
        if (usingGate)
        {
            // If the ball hasn't reached the last point on the gate...
            if(nextPoint < gateWaypoints.Length)
            {
                // If the ball hasn't reached its current point...
                if (transform.position != gateWaypoints[nextPoint].position)
                {
                    // Move to the next point on the gate.
                    transform.position = Vector2.MoveTowards(transform.position, 
                        gateWaypoints[nextPoint].position, gateSpeed * Time.deltaTime);
                }
                // Else if the ball has reached its current point...
                else
                {
                    // Move to the next point on the gate.
                    nextPoint++;
                }
            }
            // Else if the ball has finished riding the gate...
            else
            {
                // Reset all gate information.
                usingGate = false;
                nextPoint = 0;

                // Return the ball's gravity.
                rb.gravityScale = 1;

                // Enable collisions.
                circleCollider.enabled = true;

                // Throw the ball from the gate's exit.
                rb.AddForce(gateEndForce * gateSpeed, ForceMode2D.Impulse);
            }
        }
        #endregion
    }

    // This function is called by the GateTrigger.cs, which gives this script the information it needs to travel the gate.
    public void UseGateMovement(Transform[] waypoints, float speed, Vector2 endForce)
    {
        // Get variables.
        gateWaypoints = waypoints;
        gateSpeed = speed;
        gateEndForce = endForce;

        // Stop all pinball movement.
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0;
        nextPoint = 0;
        circleCollider.enabled = false;

        // Start gate movement.
        usingGate = true;
    }

    // This function is called by the NudgeFunction.cs, when a nudge is used while riding a gate.
    public void ExitGateMovement()
    {
        usingGate = false;
        nextPoint = 0;
        rb.gravityScale = 1;
        circleCollider.enabled = true;
    }

}
