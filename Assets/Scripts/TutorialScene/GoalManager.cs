using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalManager : MonoBehaviour
{
    public GameObject[] targets;
    bool transition = false;
    public AudioSource musicAudioSource;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (CheckObjective())
        {
            Time.timeScale = 0.15f;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;

            musicAudioSource.pitch = 0.5f;

            StartCoroutine(Transition());

            IEnumerator Transition()
            {
                yield return new WaitForSecondsRealtime(5f);
                Time.timeScale = 1.0f;
                Time.fixedDeltaTime = 0.02f;
                SceneManager.LoadScene("UpdateGameScene");
            }
        }
    }

    public bool CheckObjective()
    {
        for (int i = 0; i < targets.Length; i++)
        {
            if (!targets[i].GetComponent<RewardScript>().disabled)
            {
                return false;
            }
        }

        return true;
    }
}
