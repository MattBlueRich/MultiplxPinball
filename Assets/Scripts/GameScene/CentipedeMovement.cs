using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CentipedeMovement : MonoBehaviour
{
    public Transform[] waypoints;
    private int waypointCount;
    public bool isMoving = true;
    private int nextPoint = 0;
    public float moveSpeed = 5;
    public GameObject[] targets;
    private Quaternion targetRotation;
    private bool flyOut = false;

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            if (nextPoint < waypoints.Length)
            {
                if (transform.position != waypoints[nextPoint].position)
                {
                    transform.position = Vector2.MoveTowards(transform.position,
                        waypoints[nextPoint].position, moveSpeed * Time.deltaTime);                   
                }
                else
                {
                    nextPoint++;

                    transform.Rotate(0, 0, -90);
                    FixTargetRotation();
                }
            }
            else
            {
                nextPoint = 0;
            }
        }

        if (flyOut)
        {
            transform.Translate((Vector3.left * 50f) * Time.deltaTime);
        }
    }

    void FixTargetRotation()
    {
        for (int i = 0; i < targets.Length; i++)
        {
            targets[i].transform.Rotate(0, 0, -270);
        }
    }

    public void PlayDeathAnimation()
    {
        StartCoroutine(PlayFinalExplosion());
        isMoving = false;

        IEnumerator PlayFinalExplosion()
        {
            for (int i = 0; i < targets.Count(); i++)
            {
                targets[i].GetComponent<RewardScript>().PlayExplosion();
                yield return new WaitForSeconds(.25f);
            }
            yield return new WaitForSeconds(.25f);
            flyOut = true;
            Destroy(transform.parent.gameObject, 2f);
        }
    }

}
