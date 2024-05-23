using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienMovement : MonoBehaviour
{
    public float minX;
    public float maxX;
    private Vector3 targetPos;
    public float moveSpeed;
    private Vector3 velocity = Vector3.zero;
    public float smoothness = 0.25f;

    public bool isMoving = true;
    private bool moveToPoint = true;

    public RewardScript rewardScript;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(minX, transform.position.y, transform.position.z);
        targetPos = transform.position;
        isMoving = true;
    }

    // Update is called once per frame
    void Update()
    {
        // If the alien is vulnerable to being hit...
        if (!rewardScript.disabled)
        {
            // If the alien has a target pos and is allowed to begin moving...
            if (moveToPoint)
            {
                transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothness, moveSpeed);
            }
            
            // If the alien has reached the target position...
            if (transform.position == targetPos && moveToPoint)
            {
                moveToPoint = false; // Run this code once.

                StartCoroutine(SetTargetPosition()); // Set new target position.
            }
        }
    }

    IEnumerator SetTargetPosition()
    {
        yield return new WaitForSeconds(Random.Range(2, 5)); // Wait random period of time.

        // If the alien is on the minX, set targetPos to maxX, else, set targetPos to minX.
        if (targetPos.x == minX)
        {
            targetPos.x = maxX;
        }
        else
        {
            targetPos.x = minX;
        }

        moveToPoint = true; // Allow alien to start moving.
    }
}
