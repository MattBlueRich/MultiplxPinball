using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NudgeUI : MonoBehaviour
{  
    public Image cooldownFill;
    public NudgeFunction nudgeFunction;
    private float maxCooldownTime;
    private float currentCooldownTime;

    [Header("Fill Material Effects")]
    public Material strobeMaterial;
    public float strobeDelay;
    public Color strobeColourA;
    public Color strobeColourB;
    public Color completedFillColour;
    public float fillCap = 0.1f;

    private float strobeCurrentTime;
    private bool isFull = false;

    // Start is called before the first frame update
    void Start()
    {
        strobeMaterial.color = strobeColourA;
    }

    // Update is called once per frame
    void Update()
    {
        // This inversely proportions the fill amount on the UI to the NudgeFunction's cooldown value:
        maxCooldownTime = nudgeFunction.cooldownMaxDuration;
        currentCooldownTime = nudgeFunction.currentCooldownTime;
        cooldownFill.fillAmount = ((maxCooldownTime - currentCooldownTime) / maxCooldownTime) - fillCap;
       
        // If there's currently a cooldown...
        if(currentCooldownTime != 0)
        {
            isFull = false;

            // This if-statement acts as a timer for strobing the fill material.
            if (strobeCurrentTime > 0)
            {
                strobeCurrentTime -= Time.deltaTime; // Decrease current time by Time.deltaTime.
            }
            else
            {
                strobeCurrentTime = strobeDelay; // Reset current time back to max delay time. 
                UpdateMaterialColour(); // Change the material colour between colour A and colour B.
            }
        }
        // If the ability has successfully cooled down...
        else if(!isFull)
        {
            isFull = true; // This bool ensures this code is ran once.
            strobeMaterial.color = completedFillColour; // Set material colour to completed fill colour.
        }
    }

    // This function switches the fill material's colour between colours A and B, while on cooldown time.
    public void UpdateMaterialColour()
    {
        // If the material's current colour is colour A...
        if(strobeMaterial.color == strobeColourA)
        {
            strobeMaterial.color = strobeColourB; // Switch to colour B!
        }
        else
        { 
            strobeMaterial.color = strobeColourA; // Switch to colour A!
        }
    }
}
