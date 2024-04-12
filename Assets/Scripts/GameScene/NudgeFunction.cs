using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [ This script is responsible for applying force to the pinball ("nudging" or "tilting"), when the ability is made availabe after a cooldown. ] //
public class NudgeFunction : MonoBehaviour
{
    public float cooldownMaxDuration = 15f; // This is how long it takes for the nudge ability to be made available.
    public float nudgeForce; // This is how much force is applied to the pinball on nudge.
    public float currentCooldownTime; // This is how much time remains of the cooldown timer.

    private Rigidbody2D rb; // This is the pinball's RigidBody, which we will use for applying force.
    private bool canUse = false; // This bool, if true, allows the nudge ability to be used.

    public AudioClip nudgeSFX;
    AudioSource audioSource;

    private void Start()
    {
        currentCooldownTime = cooldownMaxDuration;
        
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 inputDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        
        // If the player uses the directional keys to perform a nudge, and the ability is active...
        if(inputDir != Vector2.zero && canUse)
        {
            canUse = false; // Disable ability, enabling a cooldown.
            UseNudge(inputDir); // Nudge the pinball in the input direction.
        }
        
        // Cooldown
        if (!canUse)
        {
            // Tick the cooldown time down per second.
            currentCooldownTime -= Time.deltaTime;

            // If the timer reaches zero...
            if (currentCooldownTime < 0)
            {
                canUse = true; // Enable ability to use nudge ability!
                currentCooldownTime = cooldownMaxDuration; // Reset the cooldown timer.
            }
        }

    }

    // This function pushes the ball in the desired input direction.
    public void UseNudge(Vector2 dir)
    {
        rb.AddForce(dir * nudgeForce); // Add force in direction of input.
        ScreenShake.start = true; // This shakes the screen!

        // This plays the nudge sound effect:
        audioSource.clip = nudgeSFX;
        audioSource.pitch = Random.Range(0.5f, 1f); // This makes each sound feel more distinct!
        audioSource.Play();
    }
}
