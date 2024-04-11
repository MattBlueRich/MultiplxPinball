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
        // When "start" is true, run the Shaking function once.
        if (start)
        {
            start = false;
            StartCoroutine(Shaking());
        }

        if (returning)
        {
            Vector3 targetPos = new Vector3(0, transform.position.y, -10f);

            if(transform.position != targetPos)
            {
                transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, 0.15f);
            }
            else
            {
                transform.position = targetPos;
                returning = false;
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

        returning = true;
    
    }
}
