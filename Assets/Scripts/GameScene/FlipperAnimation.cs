using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipperAnimation : MonoBehaviour
{
    public Vector2 maxAnchorPos;
    public Vector2 minAnchorPos;
    public float slowness = 2;
    private HingeJoint2D joint;

    private void Start()
    {
        joint = GetComponent<HingeJoint2D>();
    }

    void FixedUpdate()
    {
        joint.anchor = Vector2.Lerp(maxAnchorPos, minAnchorPos, Mathf.PingPong(Time.time / slowness, 1));
    }
}
