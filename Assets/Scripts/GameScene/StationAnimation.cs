using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class StationAnimation : MonoBehaviour
{
    public Vector3 maxY;
    public Vector3 minY;
    public float slowness = 1;

    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(maxY, minY, Mathf.PingPong(Time.time / slowness, 1));
    }
}
