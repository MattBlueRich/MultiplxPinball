using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Spin.cs by GameDeveloper
 * https://www.youtube.com/watch?v=Fj0ip9n_Kfc&t=736s */

public class WheelUI : MonoBehaviour
{
    [Header("Wheel Object")]
    public GameObject wheel;
    public int numberOfSegments = 12;

    [Header("Wheel Spin Physics")]  
    public float rotationTime;
    public int fullWheelRotations = 4;
    public AnimationCurve speedCurve;

    [Header("Sound Effects & Music")]
    public AudioClip wheelTickSFX;
    public AudioClip wheelMusic;
    public AudioSource managerAudioSource;

    [Header("TV Screen Material")]
    public Material TVScreenMaterial;
    public Color luckyColour;
    public Color unluckyColour;
    public float strobeDelay;
    private float strobeCurrentTime;
    private bool wheelStopped = false;

    [Header("Wheel Fate")]
    public bool isLucky;

    private const float CIRCLE = 360.0f;
    private float angleOfOneSegment;
    private float currentTime;

    AudioSource audioSource;
    Animator animator;

    public static Action onSpinWheel;
    public StatusManager statusManager;

    private void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        angleOfOneSegment = CIRCLE / numberOfSegments;

        TVScreenMaterial.color = luckyColour;
    }
    public void StartWheel()
    {
        animator.SetBool("In", true);
        managerAudioSource.Pause(); // This will pause the current gameplay music.
        audioSource.clip = wheelMusic;
        audioSource.Play();
        StartCoroutine(RotateWheel());
    }
    IEnumerator RotateWheel()
    {
        float startAngle = transform.eulerAngles.z;
        currentTime = 0;
        int indexSegmentRandom = UnityEngine.Random.Range(1, numberOfSegments); // This picks a random segment of the wheel to land on.
        IsLucky(indexSegmentRandom);

        float desiredAngle = (fullWheelRotations * CIRCLE) + angleOfOneSegment * indexSegmentRandom - startAngle;

        while (currentTime < rotationTime)
        {
            yield return new WaitForEndOfFrame();
            currentTime += Time.deltaTime;

            float angleCurrent = desiredAngle * speedCurve.Evaluate(currentTime / rotationTime);
            wheel.transform.eulerAngles = new Vector3(0, 0, angleCurrent + startAngle);
        }

        wheelStopped = true;

        if (isLucky)
        {
            TVScreenMaterial.color = luckyColour;
        }
        else
        {
            TVScreenMaterial.color = unluckyColour;
        }

        statusManager.AddStatusEffect(isLucky); // Start status effect.

        yield return new WaitForSeconds(1);
        HideWheel();
    }

    public bool IsLucky(int segmentNumber)
    {
        if(segmentNumber % 2 == 0) 
        {
            isLucky = false;
            return false;
        }
        else
        {
            isLucky = true;
            return true;
        }
    }
    public void HideWheel() 
    {
        animator.SetBool("In", false);
    }

    private void Update()
    {
        if (!wheelStopped)
        {
            // This if-statement acts as a timer for strobing the fill material.
            if (strobeCurrentTime > 0)
            {
                strobeCurrentTime -= Time.deltaTime; // Decrease current time by Time.deltaTime.
            }
            else
            {
                strobeCurrentTime = strobeDelay; // Reset current time back to max delay time. 
                UpdateMaterialColour(); // Change the material colour between colour A and colour B.
            }
        }
    }
    public void UpdateMaterialColour()
    {
        // If the material's current colour is colour A...
        if (TVScreenMaterial.color == luckyColour)
        {
            TVScreenMaterial.color = unluckyColour; // Switch to colour B!
        }
        else
        {
            TVScreenMaterial.color = luckyColour; // Switch to colour A!
        }
    }

    private void OnEnable()
    {
        onSpinWheel += StartWheel; // Adds function to the event, so that when the event is called, so is this function.
    }

    private void OnDisable()
    {
        onSpinWheel -= StartWheel; // Removes function to the event, so to avoid listening while the wheel is disabled.
    }
}

