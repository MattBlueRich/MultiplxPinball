using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public TrailRenderer trailRenderer;

    bool cooldownReset = false;

    PinballScript pinballScript;

    private void Start()
    {
        currentCooldownTime = cooldownMaxDuration;
        
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        pinballScript = GetComponent<PinballScript>();

        trailRenderer.emitting = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 inputDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        
        // If the player uses the directional keys to perform a nudge, and the ability is active...
        if(inputDir != Vector2.zero && canUse && IsInsideLevel())
        {
            canUse = false; // Disable ability, enabling a cooldown.
            trailRenderer.emitting = true;
            StartCoroutine(trailTime());
            pinballScript.ExitGateMovement(); // If the ball is currently riding a gate, exit the gate!
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
                currentCooldownTime = 0; // Lock cooldown time.
            }
        }

        // If the cooldown needs to be reset after use...
        if (cooldownReset)
        {
            // If the cooldown is still less than the max cooldown time...
            if(currentCooldownTime < cooldownMaxDuration) 
            {
                // Gradually increase current cooldown time to match max cooldown time.
                currentCooldownTime += Time.deltaTime * 5f;
            }
            // Else if the cooldown has been reset...
            else
            {
                // Set time exactly to max cooldown time.
                currentCooldownTime = cooldownMaxDuration;
                cooldownReset = false; // Stop updating for reseting the cooldown.
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
        
        cooldownReset = true; // This increases the current cooldown time back to max cooldown time, over time.
    }

    public bool IsInsideLevel()
    {
        if(SceneManager.GetActiveScene().name == "TutorialScene" || SceneManager.GetActiveScene().name == "CreditScene")
        {
            return true;
        }
        else if(transform.position.x > -15.75 && transform.position.x < 15.75)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    IEnumerator trailTime()
    {
        yield return new WaitForSeconds(.5f);
        trailRenderer.emitting = false;
    }
}
