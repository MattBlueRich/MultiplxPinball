using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelUI : MonoBehaviour
{
    Animator animator;

    public float numberOfSegments = 12;
    public float rotationTime;
    public float numberCircleRotate;

    private const float CIRCLE = 360.0f;
    private float angleOfOneSegment;


    private void Start()
    {
        animator = GetComponent<Animator>();
        StartWheel();
    }
    public void StartWheel()
    {
        animator.SetBool("In", true);
        angleOfOneSegment = CIRCLE / numberOfSegments;

    }
    public void HideWheel() 
    {
        animator.SetBool("In", false);
    }

    
}
