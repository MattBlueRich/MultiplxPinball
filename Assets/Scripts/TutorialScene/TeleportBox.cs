using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportBox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pinball"))
        {
            collision.transform.position = new Vector3(collision.transform.position.x,
                15, collision.transform.position.z);
        }
    }
}
