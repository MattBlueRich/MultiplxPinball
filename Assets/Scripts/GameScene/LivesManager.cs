using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LivesManager : MonoBehaviour
{
    [Header("Lives Properties")]
    public int maxLives = 3; // This is how many lives the pinball has before game-over.
    public static int currentLives = 10; // 10 will be the default value.
    public int livesRemaining; // This just shows currentLives in the editor.

    [Header("Score Saving")]
    public Score score;

    [Header("Lives UI")]
    public GameObject livesUI;

    bool transitioning = false;

    // Start is called before the first frame update
    void Start()
    {
        // This if-statement sets the static variable to max lives for the first time it's played.
        if(currentLives == 10)
        {
            currentLives = maxLives;
        }
        
        livesRemaining = currentLives; // This lets us see the static variable in the editor.

        // This for-loop displays the currentLives as UI images inside the livesUI empty GameObject.
        for(int i = 0; i < livesUI.transform.childCount; i++) 
        {
            if(i <= currentLives-1)
            {
                livesUI.transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                livesUI.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    // This function is called by the UnityEvent evoked from PinballScript.cs, inside the pinball GameObject.
    public void LoseLife()
    {
        // If the player has lost all of their lives...
        if(currentLives <= 0)
        {
            score.ResetScore(); // This ensures the game will start at zero when replaying again.
            Debug.Log("Game Over!"); // This is where we can transition to the game-over screen!
        }
        else
        {
            currentLives--; // Decrement the lives by one.
            score.SaveScore(); // This saves the score when reloading the scene.
           
            // This just transitions to reloading the scene after a few seconds.
            StartCoroutine(reloadScene());
            IEnumerator reloadScene()
            {
                transitioning = true;
                yield return new WaitForSeconds(5); // Transition time.
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    private void Update()
    {
        // This is used to skip the transition for reloading the scene.
        if (transitioning)
        {
            if(Input.GetKeyDown(KeyCode.Space)) 
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}
