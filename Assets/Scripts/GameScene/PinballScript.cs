using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinballScript : MonoBehaviour
{
    // Animator Variables
    private Animator animator;
    private int verticalHash;
    private Rigidbody2D rb;

    void Start()
    {
        // StringToHash() saves memory on the Animator's parameters.
        verticalHash = Animator.StringToHash("PinballY");

        // Get access to components:
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
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

    }
}
