using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinedMovementScripts : MonoBehaviour
{
    PlayerMovement inputCtrls;

    [Header("Movement")]
    public float moveSpeed;

    public float groundDrag;
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;
    
    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public Transform orientation;
    float horizontalInput;
    float verticalInput;
    Vector3 moveDirection;
    Vector3 upAxis;
    Rigidbody rb;

    private void Start()
    {
        inputCtrls = new PlayerMovement();
        inputCtrls.Player.Enable();

        readyToJump = true;

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        InputMovement();
        SpeedControl();
        
        
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;

    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    void InputMovement()
    {
        horizontalInput = inputCtrls.Player.Move.ReadValue<Vector2>().x;
        verticalInput = inputCtrls.Player.Move.ReadValue<Vector2>().y;

        if (inputCtrls.Player.Jump.WasPressedThisFrame() && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);

        }
    }


    void MovePlayer()
    {
        if (inputCtrls.Player.Sprint.WasPressedThisFrame())
        {
            moveSpeed = 10f;
        }
        else
        {
            moveSpeed = 7f;
        }

        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        else if (!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);

    }
     
    void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
    void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    void ResetJump()
    {
        readyToJump = true;
    }
}
