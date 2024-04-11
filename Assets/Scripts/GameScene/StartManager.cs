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

    void FixedUpdate()
    {
        if (!startGame && !gameStarted)
        {
            pinball.transform.position = Vector3.Lerp(pointB, pointA, Mathf.PingPong(Time.time / lerpSlowness, 1));
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            startGame = true;
            UEGameStart.Invoke();
        }

        if (startGame && !gameStarted)
        {
            gameStarted = true;
            pinball.GetComponent<Rigidbody2D>().AddForce(Vector2.up * initialForce, ForceMode2D.Impulse);
        }
    }
}
