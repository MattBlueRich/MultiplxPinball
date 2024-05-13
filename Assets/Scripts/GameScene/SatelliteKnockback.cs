using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatelliteKnockback : MonoBehaviour
{
    public float knockbackForce;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pinball"))
        {
            Vector2 direction = (this.transform.position - collision.transform.position).normalized;
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(direction * -knockbackForce, ForceMode2D.Impulse);
        }
    }

}
