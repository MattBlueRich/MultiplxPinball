using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{

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
        SceneManager.LoadScene("");
    }
    public void ExitButton()
    {
        Application.Quit();
    }

}
