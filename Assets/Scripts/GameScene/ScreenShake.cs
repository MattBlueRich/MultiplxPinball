using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* ScreenShake.cs by Thomas Friday
 * https://www.youtube.com/watch?v=BQGTdRhGmE4 */
public class ScreenShake : MonoBehaviour
{
    public static bool start = false; // This bool can be accessed from outside the script, and starts the shake effect.
    public AnimationCurve curve; // This curve graph controls the strength of the shake effect.
    public float duration = 1f; // This is how long the shake lasts.

    bool returning = false;
    Vector3 velocity = Vector3.zero;

    void Update()
    {
        // When "start" is true, run the Shaking function once ("ScreenShake.start = true").
        if (start)
        {
            start = false;
            StartCoroutine(Shaking());
        }

        // Bugfix: I added this if-statement to fix the issue where the camera fails to reset its x-axis back to zero!
        if (returning)
        {
            Vector3 targetPos = new Vector3(0, transform.position.y, -10f);

            // If the x-axis is not equal to zero...
            if(transform.position != targetPos)
            {
                // Smoothly move the camera's x-position towards zero.
                transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, 0.15f);
            }
            // If the x-axis has been reset to zero...
            else
            {
                transform.position = targetPos; // This resets the position fully to zero.
                returning = false; // The camera is no longer off-center, and this if-statement no longer needs to be called.
            }
        }
    }
    IEnumerator Shaking()
    {
        float elapsedTime = 0f; // Reset time.

        // While the current time is less than the effect's duration...
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime; // Increase time.
            float strength = curve.Evaluate(elapsedTime / duration); // Converts curve graph heights to strength float.
            // Randomly moves camera in circular shape, with an increased radius by strength float.
            transform.localPosition = transform.localPosition + Random.insideUnitSphere * strength;
            yield return null;
        }

        returning = true; // Start moving the camera's x-position back to zero in Update().
    }
}
