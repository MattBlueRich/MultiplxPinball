using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditScroll : MonoBehaviour
{
    public Transform endBox;


    private void Update()
    {
        if(endBox.position.y < -17)
        {
            SceneManager.LoadScene("MenuScene");
        }
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += new Vector3(0, -0.5f * Time.deltaTime, 0);
    }
}
