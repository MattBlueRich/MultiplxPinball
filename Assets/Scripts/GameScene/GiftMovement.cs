using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftMovement : MonoBehaviour
{
    public bool isLeft;
    private int direction;
    public float speed;

    SpriteRenderer spriteRenderer;
    GameObject pinball;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        pinball = GameObject.FindGameObjectWithTag("Pinball");

        bool pickDirection = Random.value > 0.5f;

        if(pickDirection)
        {
            direction = 1;
            spriteRenderer.flipX = true;
            transform.position = new Vector3(30, pinball.transform.position.y, 0);
            Debug.Log("left");
        }
        else
        {
            direction = -1;
            spriteRenderer.flipX = false;
            transform.position = new Vector3(-30, pinball.transform.position.y, 0);
            Debug.Log("right");
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(Vector2.left * speed * direction * Time.deltaTime);
    }
}
