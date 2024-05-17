using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeterRocks : MonoBehaviour
{
    RectTransform rectTransform;
    public float fallSpeed;


    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(rectTransform.anchoredPosition.x > -25)
        {
            Vector3 aPos = transform.position;
            aPos.x = aPos.z = 0;
            aPos.y = fallSpeed * LevelMovement.LevelSpeed * Time.deltaTime;
            transform.position -= aPos;
        }
        else
        {
            Vector3 rect = rectTransform.anchoredPosition;
            rect.x = 180f;
            rectTransform.anchoredPosition = rect;
        }
    }
}
