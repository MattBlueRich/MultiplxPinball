using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationarySprite : MonoBehaviour
{
    public List<Sprite> sprites;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        InvokeRepeating("PickNewSprite", 0, 0.5f);
    }

    public void PickNewSprite()
    {
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Count)]; // This picks a random sprite from the list.
    }
}
