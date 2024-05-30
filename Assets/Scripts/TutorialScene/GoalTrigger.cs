using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class GoalTrigger : MonoBehaviour
{
    bool goalReached = false;
    Camera mainCamera;
    public Transform level2CamTransform;
    public float speed = 5;
    bool endMovement = false;
    public GameObject pinball;
    public Transform newLevelSpawn;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pinball") && !goalReached)
        {
            goalReached = true;
            pinball.transform.position = newLevelSpawn.transform.position;
        }
    }

    private void Update()
    {
        if(mainCamera.transform.position != level2CamTransform.position && goalReached && !endMovement) 
        {
            mainCamera.transform.position = Vector3.Lerp
                (mainCamera.transform.position, level2CamTransform.position, speed * Time.deltaTime);
        }
        else if(mainCamera.transform.position == level2CamTransform.position && !endMovement)
        {
            endMovement = true;
        }
    }
}
