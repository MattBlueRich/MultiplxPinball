using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HighScoreUI : MonoBehaviour
{
    [SerializeField] GameObject HighScorePanel;
    [SerializeField] GameObject HighScoreUIHighScorePartPrefab;
    [SerializeField] Transform HighScorePartWrapper;


    List<GameObject> uiParts = new List<GameObject>();

    private void OnEnable()
    {
        HighScoreHandler.onHighScoreListChanged += UpdateUI;
    }

    private void OnDisable()
    {
        HighScoreHandler.onHighScoreListChanged -= UpdateUI;
    }

    public void ShowPanel()
    {
        HighScorePanel.SetActive(true);
    }

    public void ClosePanel()
    {
        HighScorePanel.SetActive(false);
    }

    private void UpdateUI (List<HighScorePart> list)
    {
        for (int i =0; i < list.Count; i++)
        {
            HighScorePartWrapper part = list[i];

            if (part.points > 0)
            {
                if (i >= uiParts.Count)
                {
                    //instantiate a new entry
                    var inst = Instantiate(HighScoreUIHighScorePartPrefab, Vector3.zero, Quaternion.Identity);
                    inst.transform.SetParent(HighScorePartWrapper);
                    uiParts.Add(inst);
                }

                // write or overwrite name and score
                var texts = uiParts[i].GetComponentsInChildren<Text>();
                texts[0].text = part.PlayerName;
                texts[1].text = part.Score.ToString(); ;
            }
        }
    }
}