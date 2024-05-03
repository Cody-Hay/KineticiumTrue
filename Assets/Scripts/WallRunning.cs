using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRunning : MonoBehaviour
{
    [Header("Wall Running")]

    public LayerMask WhatIsWall;
    public float wallClimbSpeed;
    public float WallJumpUpForce;
    public float WallJumpSideForce;

    public float WallRunForce;
    public float MaxWallRunTime;
    private float WallRunTimer;

    [Header("Input")]
    public KeyCode JumpKey = KeyCode.Space;
    public KeyCode upwardsRunKey = KeyCode.LeftShift;
    public KeyCode downwardsRunKey = KeyCode.LeftControl;
    private bool upwardsRunning;
    private bool downwardsRunning;
    private float HorizontalInput;
    private float VerticalInput;

    [Header("Detection")]
    public float WallCheckDistance;
    public float MinJumpHeight;
    private RaycastHit LeftWallHit;
    private RaycastHit RightWallHit;
    private bool WallRight;
    private bool WallLeft;
    [SerializeField] private bool WallusRunnus;

    [Header("References")]
    private PlayerMovement playerMovement;
    public Transform orientation;
    private Rigidbody rb;
    public CameraControls CameraControls;

  
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerMovement = GetComponent<PlayerMovement>();

    }

    // Update is called once per frame
    void Update()
    {
        CheckForWall();
        StateMachine();
    }

    private void FixedUpdate()
    {
        if (WallusRunnus)
        {
            IsWallRunning();
        }
    }

    //Wallrun check
    private void CheckForWall()
    {
        WallRight = Physics.Raycast(transform.position, orientation.right, out RightWallHit, WallCheckDistance, WhatIsWall);
        WallLeft = Physics.Raycast(transform.position, -orientation.right, out LeftWallHit, WallCheckDistance, WhatIsWall);
    }

    private void StateMachine()
    {
        HorizontalInput = Input.GetAxisRaw("Horizontal");
        VerticalInput = Input.GetAxisRaw("Vertical");

        if((WallLeft || WallRight) && VerticalInput > 0 && playerMovement.IsGrounded == false)
        {
            if (!WallusRunnus)
            {
                StartWallRun();
            }

            if (Input.GetKeyDown(JumpKey))
            {
                WallJump();
            }
        }

        else
        {
            if (WallusRunnus)
            {
               EndWallRun();
            }
        }
    }

    private void StartWallRun()
    {
        WallusRunnus =true;
    }

    //Method for during wallrunning
    private void IsWallRunning()
    {
        rb.useGravity = false;
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        Vector3 WallNormal= WallRight ? RightWallHit.normal : LeftWallHit.normal;

        Vector3 WallForward = Vector3.Cross(WallNormal, transform.up);

        if((orientation.forward-WallForward).magnitude > (orientation.forward- -WallForward).magnitude)
        {
            WallForward = -WallForward;
        }

        rb.AddForce(WallForward * WallRunForce, ForceMode.Force);

        if (upwardsRunning)
            rb.velocity = new Vector3(rb.velocity.x, wallClimbSpeed, rb.velocity.z);
        if (downwardsRunning)
            rb.velocity = new Vector3(rb.velocity.x, -wallClimbSpeed, rb.velocity.z);

        if (!(WallLeft && HorizontalInput > 0)&& !(WallRight && HorizontalInput < 0))
        {
            rb.AddForce(-WallNormal * 100, ForceMode.Force);
        }

        if(WallLeft)
        {
            CameraControls.FOVTilt(-5f);
        }

        if(WallRight)
        {
            CameraControls.FOVTilt(5f);
        }
    }

    private void EndWallRun()
    {
        rb.useGravity = true;
        WallusRunnus = false;
        CameraControls.FOVTilt(0f);
    }

    //Wall jump method
    private void WallJump()
    {
        Vector3 wallNormal = WallRight ? RightWallHit.normal : LeftWallHit.normal;
        Vector3 forceToApply = transform.up * WallJumpUpForce * 5 + wallNormal * WallJumpSideForce * 5;

        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(forceToApply, ForceMode.Force);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, -orientation.right);
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, orientation.right);
        Gizmos.color = Color.magenta;

    }
}
