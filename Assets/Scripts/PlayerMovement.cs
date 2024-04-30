using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]

    public float MoveSpeed;
    public float WallRunSpeed;
    public float GroundDrag;
    public float TopXSpeed;
    public float TopYSpeed;
    public float TopZSpeed;

    public float JumpForce;
    public float JumpCooldown;
    public float AirMultiplier;

    private bool Accelerating;
    float HorizontalInput;
    float VerticalInput;

    public float DamageMultiplier;

    public float DamageReduction;

    public Transform Orientation;

    Vector3 MoveDirection;

    Rigidbody rb;

    [Header("GroundCheck")]

    //public float PlayerHeight;
    public Transform FootPos;
    public float CheckRadius;
    public LayerMask WhatIsGround;
    public bool IsGrounded;
    public float PlayerHeight;

    public CharacterStates currentState;

    [Header("UI Elements")]

    public TMP_Text Speedometer;
    public Slider SpeedSlider;
    public float CurrentVelocity;

    public CameraControls _cameraControls;

    public float roundedVelocity;

    public enum CharacterStates
    {
        walking,
        sprinting,
        wallrunning,
        crouching,
        sliding
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        WallRunSpeed= MoveSpeed;
    }

   
    void Update()
    {
        //Ground Check
        IsGrounded = Physics.Raycast(transform.position, Vector3.down, PlayerHeight * 0.5f + 0.2f, WhatIsGround);
        
        MyInput();

        //Speedometer
        CurrentVelocity = rb.velocity.magnitude;
        roundedVelocity = Mathf.Round(CurrentVelocity);
        Speedometer.text = roundedVelocity.ToString();
        SpeedSlider.value = roundedVelocity / 30;
        DamageMultiplier = roundedVelocity * 0.25f;
        DamageReduction = roundedVelocity / 5;
        SpeedToFOV(roundedVelocity);

    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    //Input Manager
    private void MyInput()
    {
        HorizontalInput = Input.GetAxisRaw("Horizontal");
        VerticalInput = Input.GetAxisRaw("Vertical");

        if(Input.GetKeyDown(KeyCode.Space) && IsGrounded)
        {
            Jump();
        }
        if (Input.GetKey(KeyCode.F))
        {
            rb.drag = GroundDrag;
        }
        else
        {
            rb.drag = 0;
        }
    }

    //Movement Behaviour
    private void MovePlayer()
    {
        MoveDirection = Orientation.forward * VerticalInput + Orientation.right * HorizontalInput;

        if(IsGrounded)
        {
            //rb.AddForce(MoveDirection.normalized * MoveSpeed * 10f, ForceMode.Force);
            rb.velocity += MoveDirection * (MoveSpeed * 1.5f * Time.deltaTime);
        }
        else if(!IsGrounded)
        {
            rb.velocity += MoveDirection * (MoveSpeed * 1.5f * Time.deltaTime);
        }

        rb.velocity = new Vector3(
            Mathf.Clamp(rb.velocity.x, (-TopXSpeed), TopXSpeed),
            Mathf.Clamp(rb.velocity.y,  -TopYSpeed, TopYSpeed),
            Mathf.Clamp(rb.velocity.z, (-TopZSpeed), TopZSpeed));
    }

    //Jump Behaviour
    private void Jump()
    {
        rb.velocity = new Vector3 (rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * JumpForce, ForceMode.Impulse);
    }

    private void SpeedToFOV(float playerspeed)
    {
        
       float DividedSpeed = playerspeed / 27 + 0.1f;
        float desiredFOV = DividedSpeed * 80;
        _cameraControls.FOVChange(desiredFOV);
    }
}
