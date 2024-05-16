using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelKillBox : MonoBehaviour
{
    public GameObject[] EasyLevels;
    public GameObject[] MediumLevels;
    public GameObject[] HardLevels;

    public float spawnheight = 73;
    public GameObject gridParent;
    private string currentDifficulty;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Level"))
        {
            Debug.Log("Kill Level");
            Destroy(collision.gameObject);

            switch (ProgressionManager.difficulty)
            {
                case difficulties.Easy:
                    currentDifficulty = "Easy";
                    break;
                case difficulties.Medium:
                    currentDifficulty = "Medium";
                    break;
                case difficulties.Hard:
                    currentDifficulty = "Hard";
                    break;

            }

            GameObject level = Instantiate(PickRandomLevel(currentDifficulty), gridParent.transform);
            level.transform.position = new Vector3(level.transform.position.x, spawnheight);
        }
    }

    public GameObject PickRandomLevel(string difficulty)
    {
        switch (difficulty)
        {
            case "Easy":
                return EasyLevels[Random.Range(0, EasyLevels.Length)];
            case "Medium":
                return MediumLevels[Random.Range(0, MediumLevels.Length)];
            case "Hard":
                return HardLevels[Random.Range(0, HardLevels.Length)];
        }
        return null;        
    }
   
}
