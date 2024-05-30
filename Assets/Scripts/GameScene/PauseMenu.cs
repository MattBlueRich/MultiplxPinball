using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseUI;
    public GameObject cautionUI;

    public AudioLowPassFilter musicFilter;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        pauseUI.SetActive(false);
        cautionUI.SetActive(false);
        Cursor.visible = false;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(Time.timeScale == 0.0f)
            {
                pauseUI.SetActive(false);
                cautionUI.SetActive(false);
                Time.timeScale = 1.0f;
                musicFilter.enabled = false;
                Cursor.visible = false;
            }
            else
            {
                pauseUI.SetActive(true);
                Time.timeScale = 0.0f;
                musicFilter.enabled = true;
                Cursor.visible = true;
            }                      
        }
    }

    public void OpenCautionMenu()
    {
        pauseUI.SetActive(false);
        cautionUI.SetActive(true);
    }

    public void ExitToMenu()
    {
        Time.timeScale = 1.0f;
        Cursor.visible = false;
        SceneManager.LoadScene("MenuScene");
    }
    public void ResumeGame()
    {
        pauseUI.SetActive(false);
        cautionUI.SetActive(false);
        Time.timeScale = 1.0f;
        musicFilter.enabled = false;
        Cursor.visible = false;
    }
    public void PlayHoverSound(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.pitch = Random.Range(0.7f, 1.0f);
        audioSource.Play();
    }
}
