using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    AudioSource audioSource;
    public Toggle tutorialToggle;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Cursor.visible = true;

        //PlayerPrefs.SetInt("PlayedTutorial", 0);

        if (PlayerPrefs.GetInt("PlayedTutorial", 0) == 1)
        {
            tutorialToggle.gameObject.SetActive(true);
        }
        else
        {
            tutorialToggle.gameObject.SetActive(false);
        }
        
    }

    public void StartButton()
    {
        Cursor.visible = false;

        if(PlayerPrefs.GetInt("PlayedTutorial", 0) == 1)
        {
            SceneManager.LoadScene("UpdateGameScene");
        }
        else
        {
            PlayerPrefs.SetInt("PlayedTutorial", 1);
            SceneManager.LoadScene("TutorialScene");
        }
    }
    public void LeaderboardButton()
    {
        //SceneManager.LoadScene("");
        //loads the leaderboard scene on pressing the leaderboard button
    }
    public void CreditButton()
    {
        Cursor.visible = false;
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

    public void EnableTutorial(bool enable)
    {
        if (enable)
        {
            PlayerPrefs.SetInt("PlayedTutorial", 0);
        }
        else
        {
            PlayerPrefs.SetInt("PlayedTutorial", 1);
        }
    }
}
