using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using Unity.VisualScripting;

public class Score : MonoBehaviour
{
    [Header("Score")]
    public TextMeshProUGUI scoreText;
    public float score;
    private float _score;
    bool tickScore = false;
    public static float startingScore = 0;

    [Header("Jackpot Letters")]
    public List<char> jackpotLetters = new List<char>() { 'M', 'U', 'L', 'T', 'I', 'P', 'L', 'X', };
    public List<char> jackpotLettersRemaining;

    
    [Header("Gate Movement Detection")]
    public PinballScript pinballScript;

    private void Awake()
    {
        jackpotLettersRemaining = new List<char>(jackpotLetters);
    }
    private void Start()
    {
        score = startingScore;
        _score = score;
        scoreText.text = score.ToString();
    }

    private void Update()
    {
        // This if-statement ticks the score.
        if(_score < score)
        {
            tickScore = true;
            _score += Time.deltaTime * 150f;
            scoreText.text = _score.ToString("F0");
        }
        else if(tickScore)
        {
            tickScore = false;
            _score = score;
            scoreText.text = _score.ToString("F0");
        }

        // While inside a gate, this if-statement increments the score.
        if (pinballScript.usingGate)
        {
            score += Time.deltaTime * 20;
        }
    }

    public void AddScore(int scoreValue)
    {
        score += scoreValue;       
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

    // These functions are called by the LivesManager.cs to save score with lives, and reset on game-over with no lives.
    public void SaveScore()
    {
        startingScore = score;
    }
    public void ResetScore()
    {
        startingScore = 0;
    }
}
