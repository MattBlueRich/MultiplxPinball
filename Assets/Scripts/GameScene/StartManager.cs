using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StartManager : MonoBehaviour
{
    [Header("Pinball Starting Lerp")]
    public Vector2 pointA;
    public Vector2 pointB;
    public GameObject pinball;
    public float lerpSlowness;
    public float initialForce = 10f;

    [Header("Starting Event")]
    public UnityEvent UEGameStart;

    private bool startGame = false;
    private bool gameStarted = false;

    private void Start()
    {
        LevelMovement.LevelSpeed = 0f;
    }

    void FixedUpdate()
    {
        if (!startGame && !gameStarted)
        {
            pinball.transform.position = Vector3.Lerp(pointB, pointA, Mathf.PingPong(Time.time / lerpSlowness, 1));
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Shoot All") && !startGame)
        {
            startGame = true;
            LevelMovement.LevelSpeed = 1.0f;
            UEGameStart.Invoke(); // Trigger public game start event.
        }

        if (startGame && !gameStarted)
        {
            gameStarted = true;
            pinball.GetComponent<Rigidbody2D>().AddForce(Vector2.up * initialForce, ForceMode2D.Impulse);
        }
    }
}
