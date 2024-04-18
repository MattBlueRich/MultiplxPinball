using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotatetwo : MonoBehaviour
{
    public float speedPersec = 60f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float rotAmout = speedPersec * Time.deltaTime;
        float curRot = transform.rotation.eulerAngles.z;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, curRot + rotAmout));
    }
}

