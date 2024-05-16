using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float torque;
    [SerializeField] bool isLeft;

    public PinballScript pinballScript;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!pinballScript.usingGate)
        {
            if (GetInput())
            {
                rb.AddTorque(torque, ForceMode2D.Force);
            }
        }
    }

    bool GetInput()
    {
        if (Input.GetKey(KeyCode.Space) || Input.GetButton("Shoot All"))
        {
            return true;
        }

        else
        {
            if (isLeft)
            {
                return Input.GetKey(KeyCode.LeftControl) || Input.GetButton("Left Shoot") || Mathf.Round(Input.GetAxisRaw("Triggers")) < 0;
            }
            else
            {
                return Input.GetKey(KeyCode.RightControl) || Input.GetButton("Right Shoot") || Mathf.Round(Input.GetAxisRaw("Triggers")) > 0;
            }
        }
    }
}
