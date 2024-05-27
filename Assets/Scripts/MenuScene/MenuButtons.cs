using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void StartButton()
    {
        SceneManager.LoadScene("UpdateGameScene");
    }
    public void LeaderboardButton()
    {
        SceneManager.LoadScene("");
    }
    public void CreditButton()
    {
        SceneManager.LoadScene("CreditScene");
    }
    public void ExitButton()
    {
        Application.Quit();
    }
    public void PlayHoverSound(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.pitch = Random.Range(0.7f, 1.0f);
        audioSource.Play();  
    }
}
