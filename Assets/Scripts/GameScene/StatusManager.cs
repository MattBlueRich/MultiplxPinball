using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Want to add a new status effect? Follow the three step comment guide below:
public class StatusManager : MonoBehaviour
{
    // Step 1: Add status name to either lucky, or unlucky.
    private List<string> LuckyEffects = new List<string>() { "DP" };
    private List<string> UnluckyEffects = new List<string>() { "IC" };

    public static int multiplier = 1;
    private float statusDuration;

    public AudioSource wheelAudioSource;
    public AudioSource managerAudioSource;
    private AudioSource statusAudioSource;
    [Header("Sound Effects")]
    public AudioClip cameraShiftingSFX;
    public AudioClip doublePointsSFX;

    bool rotateCam = false;
    private Camera mainCam;
    private Quaternion invertAngle = new Quaternion(0, 0, 180, 1);
    private bool invert = false;


    private void Start()
    {
        mainCam = Camera.main;
        statusAudioSource = GetComponent<AudioSource>();
    }

    public string PickStatusEffect(bool isLucky)
    {
        if (isLucky)
        {
            return LuckyEffects[Random.Range(0, LuckyEffects.Count)];
        }
        else 
        {
            return UnluckyEffects[Random.Range(0, UnluckyEffects.Count)];
        }
    }
    public void AddStatusEffect(bool isLucky)
    {
        // Step 2: Add status name from list as a case, and add the effect and duration inside.
        switch(PickStatusEffect(isLucky)) 
        {
            /// Double Points
            case "DP":
                multiplier = 2;
                statusDuration = 10.0f;
                statusAudioSource.PlayOneShot(doublePointsSFX);
                Debug.Log("DOUBLE POINTS!");
                break;
            /// Invert Camera
            case "IC":
                
                invert = true;
                rotateCam = true;
                ScreenShake.start = true;
                
                statusDuration = 10.0f;

                statusAudioSource.clip = cameraShiftingSFX;
                statusAudioSource.loop = true;
                statusAudioSource.Play();

                Debug.Log("INVERT CAMERA");
                break;
        }       

        StartCoroutine(statusTime());

        IEnumerator statusTime()
        {
            yield return new WaitForSeconds(statusDuration);
            ResetAllStatus();
            managerAudioSource.UnPause();
            wheelAudioSource.Stop();
        }
    }

    // Step 3: Reset the effect you've added in this function. Now test it out! :)
    public void ResetAllStatus()
    {
        /// Reset multiplier
        multiplier = 1;

        /// Reset camera

        if (invert)
        {
            invert = false;
            rotateCam = true;
            ScreenShake.start = true;
            statusAudioSource.clip = cameraShiftingSFX;
            statusAudioSource.loop = true;
            statusAudioSource.Play();
        }
    }

    private void Update()
    {
        if (rotateCam)
        {
            InvertCamera(invert);
        }
    }

    public void InvertCamera(bool invert)
    {
        if (invert)
        {
            if (mainCam.transform.eulerAngles.z != 180)
            {
                float angle = Mathf.MoveTowardsAngle(mainCam.transform.eulerAngles.z, 180, 50 * Time.deltaTime);
                mainCam.transform.eulerAngles = new Vector3(0, 0, angle);
            }
            else
            {
                rotateCam = false;
                statusAudioSource.loop = false;
            }
        }
        else
        {
            if (mainCam.transform.eulerAngles.z != 0)
            {
                float angle = Mathf.MoveTowardsAngle(mainCam.transform.eulerAngles.z, 0, 200 * Time.deltaTime);
                mainCam.transform.eulerAngles = new Vector3(0, 0, angle);
            }
            else
            {
                rotateCam = false;
                statusAudioSource.loop = false;
            }
        }

    }

}
