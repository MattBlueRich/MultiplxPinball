using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float torque;
    [SerializeField] bool isLeft;

    public PinballScript pinballScript;
    public Animator swishEffect;
    float animationDelay = .2f;
    bool canAnimate = true;

    public AudioClip[] flipperSFXS;
    AudioSource audioSource;
    public bool canPlayAudio;

    bool triggerDown = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (canPlayAudio)
        {
            audioSource = GetComponent<AudioSource>();
        }      
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!pinballScript.usingGate)
        {
            if (GetInput())
            {
                rb.AddTorque(torque, ForceMode2D.Force);
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Shoot All"))
        {
            if (canAnimate)
            {
                swishEffect.SetTrigger("Flip");
                canAnimate = false;
                StartCoroutine(swishDelay());
            }
            if (canPlayAudio)
            {
                PlayRandomSFX();
            }
        }

        if (isLeft)
        {
            if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetButtonDown("Left Shoot") || Mathf.Round(Input.GetAxisRaw("Triggers")) == -1)
            {
                if (!triggerDown)
                {
                    triggerDown = true;

                    if (canAnimate)
                    {
                        swishEffect.SetTrigger("Flip");
                        canAnimate = false;
                        StartCoroutine(swishDelay());
                    }
                    if (canPlayAudio)
                    {
                        PlayRandomSFX();
                    }
                }               
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.RightControl) || Input.GetButtonDown("Right Shoot") || Mathf.Round(Input.GetAxisRaw("Triggers")) == 1)
            {
                if (!triggerDown)
                {
                    triggerDown = true;

                    if (canAnimate)
                    {
                        swishEffect.SetTrigger("Flip");
                        canAnimate = false;
                        StartCoroutine(swishDelay());
                    }
                    if (canPlayAudio)
                    {
                        PlayRandomSFX();
                    }
                }                
            }
        }

        if(Mathf.Round(Input.GetAxisRaw("Triggers")) == 0)
        {
            triggerDown = false;
        }
    }

    bool GetInput()
    {
        if (Input.GetKey(KeyCode.Space) || Input.GetButton("Shoot All"))
        {
            return true;
        }

        else
        {
            if (isLeft)
            {
                return Input.GetKey(KeyCode.LeftControl) || Input.GetButton("Left Shoot") || Mathf.Round(Input.GetAxisRaw("Triggers")) < 0;
            }
            else
            {
                return Input.GetKey(KeyCode.RightControl) || Input.GetButton("Right Shoot") || Mathf.Round(Input.GetAxisRaw("Triggers")) > 0;
            }
        }
    }

    IEnumerator swishDelay()
    {
        yield return new WaitForSeconds(animationDelay);
        canAnimate = true;
    }

    public void PlayRandomSFX()
    {
        audioSource.pitch = Random.Range(0.7f, 1f);
        audioSource.clip = flipperSFXS[Random.Range(0, flipperSFXS.Length)];
        audioSource.Play();
    }
}
