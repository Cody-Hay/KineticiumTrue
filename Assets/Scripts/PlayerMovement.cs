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

    [Header("Sliding")]
    public float MaxSlideTime;
    public float SlideForce;
    private float SlideTimer;

    public Transform PlayerObj;

    public float YScale;
    public float NormalYScale;
    private float StartYScale;

    public KeyCode SlideKey = KeyCode.LeftControl;

    private bool IsSliding;

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

        if(roundedVelocity >= 10)
        {
            _cameraControls.Highspeed = true;
            _cameraControls.PeakSpeed = false;
        }
        if(roundedVelocity<10)
        {
            _cameraControls.Highspeed = false;
            _cameraControls.PeakSpeed = false;
        }
        if (roundedVelocity >= 17)
        {
            _cameraControls.Highspeed = false;
            _cameraControls.PeakSpeed = true;
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
        if(IsSliding)
        {
            WhileSliding();
        }
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

        if (Input.GetKeyDown(SlideKey) && (HorizontalInput !=0 || VerticalInput !=0))
        {
            StartSlide();
        }
        if(Input.GetKeyUp(SlideKey) && IsSliding)
        {
            EndSlide();
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

    private void StartSlide()
    {
        IsSliding = true;

        PlayerObj.localScale = new Vector3(PlayerObj.localScale.x, YScale, PlayerObj.localScale.z);
        rb.AddForce(Vector3.down * 5f,ForceMode.Impulse);

        SlideTimer = MaxSlideTime;
    }

    private void WhileSliding()
    {
        Vector3 InputDirection = Orientation.forward * VerticalInput + Orientation.right * HorizontalInput;
        rb.AddForce(InputDirection.normalized * SlideForce, ForceMode.Force);

        SlideTimer -= Time.deltaTime;

        if(SlideTimer <= 0)
        {
            EndSlide();
        }
    }

    private void EndSlide()
    {
        IsSliding = false;
        PlayerObj.localScale = new Vector3(PlayerObj.localScale.x, PlayerObj.localScale.y, PlayerObj.localScale.z);
    }
}
