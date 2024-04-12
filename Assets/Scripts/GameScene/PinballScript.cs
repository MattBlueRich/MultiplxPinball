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

    [Header("Pinball Game Over")]
    public float deathHeight = -13f;
    public AudioClip deathSFX;

    public UnityEvent UEOutGameOver;

    private AudioSource audioSource;
    private bool gameOver = false;

    void Start()
    {
        // StringToHash() saves memory on the Animator's parameters.
        verticalHash = Animator.StringToHash("PinballY");

        // Get access to components:
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
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

        if(transform.position.y <= deathHeight && !gameOver)
        {
            gameOver = true;

            UEOutGameOver.Invoke(); // Trigger public game over event.

            audioSource.PlayOneShot(deathSFX);
        }
    }
}
