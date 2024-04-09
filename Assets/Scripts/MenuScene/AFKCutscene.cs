using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AFKCutscene : MonoBehaviour
{
    public float SecondsTillAFK; // Time till the player is AFK.
    
    private float cursorPos; 
    private float timeLeft;
    private bool isAFK = false;

    private void Start()
    {
        timeLeft = SecondsTillAFK;
    }
    private void Update()
    {
        // If the player is AFK for more than SecondsTillAFK...
        if (isAFK)
        {
            isAFK = false;
            cursorPos = Input.GetAxis("Mouse X");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1); // Transition back to CutsceneScene.
        }

        // If the mouse isn't moving...
        if (Input.GetAxis("Mouse X") == cursorPos)
        {   
            timeLeft -= Time.deltaTime; // Decrease the time.

            // If the time has decreased below zero...
            if (timeLeft < 0)
            {
                timeLeft = SecondsTillAFK; // Reset the time.
                isAFK = true; // The player has gone AFK!
            }
        }
        // Else if the mouse has moved...
        else
        {
            timeLeft = SecondsTillAFK; // Reset the time.
        }

        // If the keyboard has been pressed...
        if (Input.anyKeyDown)
        {
            timeLeft = SecondsTillAFK; // Reset the time.
        }
    }
}
