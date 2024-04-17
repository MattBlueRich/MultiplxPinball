using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilemapFocus : MonoBehaviour
{
    [Header("Gate Focus Properties")]
    public Material tilemapMaterial;
    public float focusSpeed = 10f;
    public float unfocusTransparency = 0.1f;

    private float targetAlpha = 1;
    private float currentAlpha;    

    bool isFocusing = false;
    bool focusIn = true;

    // Start is called before the first frame update
    void Start()
    {
        tilemapMaterial.color = new Color(1, 1, 1, targetAlpha);
        currentAlpha = targetAlpha;
    }

    // Update is called once per frame
    void Update()
    {
        // If we want to change the transparency of the level...
        if (isFocusing)
        {
            // If we want to focus out (make transparent)...
            if (!focusIn) 
            {
                // While the transparency is above our target transparency...
                if (tilemapMaterial.color.a > unfocusTransparency)
                {
                    // Decrease alpha.
                    currentAlpha -= Time.deltaTime * focusSpeed;
                    tilemapMaterial.color = new Color(1, 1, 1, currentAlpha);
                }
                else
                {
                    // Set values and exit if-loop.
                    currentAlpha = unfocusTransparency;
                    tilemapMaterial.color = new Color(1, 1, 1, currentAlpha);
                    isFocusing = false;
                }
            }
            // Else if we want to focus in (make opaque)...
            else if (focusIn)
            {
                // While the transparency is less than 1...
                if (tilemapMaterial.color.a < 1f)
                {
                    // Increase alpha.
                    currentAlpha += Time.deltaTime * focusSpeed;
                    tilemapMaterial.color = new Color(1, 1, 1, currentAlpha);
                }
                else
                {
                    // Set values and exit if-loop.
                    currentAlpha = 1f;
                    tilemapMaterial.color = new Color(1, 1, 1, currentAlpha);
                    isFocusing = false;
                }
            }
        }
    }

    // The Unfocus() and Focus() methods are called by PinballScript.cs when entering/exiting a gate.
    public void Unfocus()
    {
        isFocusing = true;
        focusIn = false;
    }

    public void Focus()
    {
        isFocusing = true;
        focusIn = true;
    }
}
