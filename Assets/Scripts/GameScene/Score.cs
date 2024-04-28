using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using Unity.VisualScripting;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public int score;

    public List<char> jackpotLetters = new List<char>() { 'M', 'U', 'L', 'T', 'I', 'P', 'L', 'X', };
    public List<char> jackpotLettersRemaining;

    private void Awake()
    {
        jackpotLettersRemaining = new List<char>(jackpotLetters);
    }
    private void Start()
    {
        score = 0; // Subject to change with a lives system.       
    }

    public void AddScore(int scoreValue)
    {
        score += scoreValue;
        scoreText.text = score.ToString();
    }

    // This is called by the RewardScript.cs, when colliding with a jackpot letter target.
    public void AddLetter(char letter) 
    {
        // Remove letter if it exists in the list of letters remaining.
        if (jackpotLettersRemaining.Contains(letter))
        {
            jackpotLettersRemaining.Remove(letter);
        }
        else
        {
            return;
        }

        // If there are no more jackpot letters left, award the jackpot and reset remaining letters.
        if(jackpotLettersRemaining.Count == 0)
        {
            AddScore(1500); // Award jackpot value.
            jackpotLettersRemaining = jackpotLetters;
        }
    }
}
