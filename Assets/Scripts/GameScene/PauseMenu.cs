using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseUI;
    public GameObject cautionUI;

    // Start is called before the first frame update
    void Start()
    {
        pauseUI.SetActive(false);
        cautionUI.SetActive(false);
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
            }
            else
            {
                pauseUI.SetActive(true);
                Time.timeScale = 0.0f;
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
        SceneManager.LoadScene("MenuScene");
    }
    public void ResumeGame()
    {
        pauseUI.SetActive(false);
        cautionUI.SetActive(false);
        Time.timeScale = 1.0f;
    }
}
