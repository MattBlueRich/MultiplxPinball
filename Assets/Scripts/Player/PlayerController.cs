using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float torque;
    [SerializeField] float motorspeed = 100;
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            return true;
        }

        else
        {
            if (isLeft)
            {
                return Input.GetKeyDown(KeyCode.LeftShift);
            }
            else
            {
                return Input.GetKeyDown(KeyCode.RightShift);
            }
        }
    }
}
