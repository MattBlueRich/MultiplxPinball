using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum difficulties
{
    Easy,
    Medium,
    Hard
}

public class ProgressionManager : MonoBehaviour
{
    [Header("Level Speed Progression")]
    public float levelSpeedFactor;
    public float maxLevelSpeed;
    private float currentTime;

    [Header("Level Selection Progression")]
    public static difficulties difficulty;
    public float easyLevelDuration;
    public float mediumLevelDuration;

    [Header("Other")]
    public PinballScript pinballScript;

    public AudioSource managerAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        difficulty = difficulties.Easy; // Start game on Easy difficulty.
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!pinballScript.usingGate && LevelMovement.LevelSpeed < maxLevelSpeed)
        {
            // This increments the speed of the level movement over time by a factor.
            LevelMovement.LevelSpeed += Time.deltaTime * levelSpeedFactor;
            currentTime += Time.deltaTime;

            // This switch-statment changes the difficulty over time.
            switch (difficulty)
            {
                case difficulties.Easy:
                    if (currentTime >= easyLevelDuration)
                    {
                        Debug.Log("Switching difficulty to medium!");
                        currentTime = 0;
                        difficulty = difficulties.Medium;
                        managerAudioSource.pitch += 0.1f;
                    }
                    break;
                case difficulties.Medium:
                    if (currentTime >= mediumLevelDuration)
                    {
                        Debug.Log("Switching difficulty to hard!");
                        currentTime = 0;
                        difficulty = difficulties.Hard;
                        managerAudioSource.pitch += 0.1f;
                    }
                    break;
            }
        }
    }
}
