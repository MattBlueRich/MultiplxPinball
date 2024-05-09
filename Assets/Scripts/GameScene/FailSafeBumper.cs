using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailSafeBumper : MonoBehaviour
{
    bool startFalling = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (startFalling)
        {
            transform.position += new Vector3(0, -0.5f * Time.deltaTime, 0);      
        }        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pinball"))
        {
            startFalling = true;
            Destroy(gameObject, 10f);
        }
    }
}
