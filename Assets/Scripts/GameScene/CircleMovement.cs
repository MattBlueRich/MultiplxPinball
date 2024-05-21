using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleMovement : MonoBehaviour
{
    [SerializeField]
    float moveSpeed, radius;
    float angle;
    void FixedUpdate()
    {
        angle += moveSpeed * Time.deltaTime;
        transform.position = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
    }

    private void Update()
    {
        if (transform.GetChild(0).GetComponent<RewardScript>().disabled)
        {
            gameObject.SetActive(false);
        }
    }
}
