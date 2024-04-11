using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NudgeFunction : MonoBehaviour
{
    public float cooldownMaxDuration = 15f;
    public Rigidbody2D pinballRB;
    public float nudgeForce;
    public float currentCooldownTime;

    private bool canUse = false;

    private void Start()
    {
        currentCooldownTime = cooldownMaxDuration;
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
            currentCooldownTime -= Time.deltaTime;

            if (currentCooldownTime < 0)
            {
                canUse = true;
                currentCooldownTime = cooldownMaxDuration;
            }
        }

    }

    public void UseNudge(Vector2 dir)
    {
        pinballRB.AddForce(dir * nudgeForce);
        ScreenShake.start = true;
    }
}
