using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Sliding : MonoBehaviour
{
    [Header("References")]
    public Transform Orientation;
    public Transform PlayerObj;
    private Rigidbody rb;
    private PlayerMovement PMovement;

    [Header("Sliding")]
    public float MaxSlideTime;
    public float SlideForce;
    private float SlideTimer;

    public float YScale;
    private float StartYScale;

    [Header("Input")]
    public KeyCode SlideKey = KeyCode.LeftControl;
    private float HorizontalInput;
    private float VerticalInput;

    private bool IsSliding;

    // Start is called before the first frame update
    void Start()
    {
        rb= GetComponent<Rigidbody>();
        PMovement= GetComponent<PlayerMovement>();

        StartYScale =PlayerObj.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void StartSlide()
    {

    }

    private void WhileSliding()
    {

    }

    private void EndSlide()
    {

    }
}
