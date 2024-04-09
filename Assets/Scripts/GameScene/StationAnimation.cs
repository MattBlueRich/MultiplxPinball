using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class StationAnimation : MonoBehaviour
{
    public Vector3 maxY;
    public Vector3 minY;
    public float slowness = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(maxY, minY, Mathf.PingPong(Time.time / slowness, 1));
    }
}
