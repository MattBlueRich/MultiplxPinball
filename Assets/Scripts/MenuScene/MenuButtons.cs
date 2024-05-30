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
        //loads the game scene on pressing the start button
    }
    public void LeaderboardButton()
    {
        SceneManager.LoadScene("");
        //loads the leaderboard scene on pressing the leaderboard button
    }
    public void CreditButton()
    {
        SceneManager.LoadScene("CreditScene");
        //loads the credits on pressing the credits button
    }
    public void ExitButton()
    {
        Application.Quit();
        //closes the game on pressing the exit button
    }
    public void PlayHoverSound(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.pitch = Random.Range(0.7f, 1.0f);
        audioSource.Play();  
    }
}
