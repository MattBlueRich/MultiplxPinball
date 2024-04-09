using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class CutsceneSkip : MonoBehaviour
{
    VideoPlayer player;
    private void Start()
    {
        player = GetComponent<VideoPlayer>();
        player.loopPointReached += OnVideoFinished;
    }

    void Update()
    {
        // If the player presses any key...
        if (Input.anyKeyDown)
        {
            // Transition to the main menu scene when skipped.
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        } 
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        // Transition to the main menu scene when video is over.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
