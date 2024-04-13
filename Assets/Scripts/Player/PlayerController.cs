using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float torque;
    [SerializeField] bool isLeft;
    private KeyCode[] flipKey;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GetInput())
        {
            rb.AddTorque(torque, ForceMode2D.Force);                           
        }
    }

    bool GetInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            return true;
        }

        else
        {
            if (isLeft)
            {
                return Input.GetKey(KeyCode.LeftShift);
            }
            else
            {
                return Input.GetKey(KeyCode.RightShift);
            }
        }
    }
}
