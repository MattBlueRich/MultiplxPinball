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

    [Header("Wheel Fate")]
    public bool isLucky;

    private const float CIRCLE = 360.0f;
    private float angleOfOneSegment;
    private float currentTime;

    AudioSource audioSource;
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        angleOfOneSegment = CIRCLE / numberOfSegments;
    }

    [ContextMenu("Start Wheel")] // By right-clicking this script, this function can be called in the editor.
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
        int indexSegmentRandom = Random.Range(1, numberOfSegments); // This picks a random segment of the wheel to land on.
        IsLucky(indexSegmentRandom);

        float desiredAngle = (fullWheelRotations * CIRCLE) + angleOfOneSegment * indexSegmentRandom - startAngle;

        while (currentTime < rotationTime)
        {
            yield return new WaitForEndOfFrame();
            currentTime += Time.deltaTime;

            float angleCurrent = desiredAngle * speedCurve.Evaluate(currentTime / rotationTime);
            wheel.transform.eulerAngles = new Vector3(0, 0, angleCurrent + startAngle);
        }

        /*
        Here is where we will pick from a list of different status effects, either good or bad depending on the bool isLucky.
        We will probably set the status and cooldown of the status in another script!
        */

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
}
