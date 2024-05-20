using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JackpotLetterUI : MonoBehaviour
{
    public List<GameObject> jackpotLettersUI;
    // This list doesn't change and is the default.
    private List<char> fullJackpotLetters = new List<char>() { 'M', 'U', 'L', 'T', 'I', 'P', 'L', 'X', };
    // This list is being changed.
    private List<char> jackpotLetters = new List<char>() { 'M', 'U', 'L', 'T', 'I', 'P', 'L', 'X', };

    private void Start()
    {
        ResetUI(); // Jackpot letters UI reset when new game starts.
    }
    public void ResetUI()
    {
        for(int i = 0; i < jackpotLettersUI.Count; i++) 
        {
            jackpotLettersUI[i].SetActive(false);
            jackpotLetters.Clear();
            jackpotLetters.AddRange(fullJackpotLetters);
        }
    }

    // This is called by Score.cs, when a letter is removed from the current letters list.
    public void UpdateUI(char jackpotLetter)
    {
        for(int i = 0; i < jackpotLetters.Count; i++)
        {
            if (jackpotLetters[i] == jackpotLetter)
            {
                jackpotLetters[i] = 'Z'; // This will just make sure to ignore this letter for now on once collected.
                jackpotLettersUI[i].SetActive(true);
                break;
            }
        }
    }
}
