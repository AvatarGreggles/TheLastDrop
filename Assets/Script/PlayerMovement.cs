using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D theRB;
    public float moveSpeed;
    public float jumpForce;
    public Transform groundCheckPoint;
    private bool isOnGround;
    public LayerMask whatIsGround;
    private bool canDoubleJump;
    private float jumpBufferTime = 0.2f;
    float jumpBufferCounter = 0f;
    private float coyoteTime = 1f;
    float coyoteTimeCounter = 0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        HandleMovement();

        // Checking if on the ground
        isOnGround = Physics2D.OverlapCircle(groundCheckPoint.position, .3f, whatIsGround);

        if (isOnGround)
        {
            Debug.Log("OnGround");
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            Debug.Log("Not on ground");
            coyoteTimeCounter -= Time.deltaTime;
        }

        HandleJumping();
    }

    void HandleMovement()
    {
        // Move sideways
        theRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, theRB.velocity.y);

        // Handle direction change
        if (theRB.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (theRB.velocity.x > 0)
        {
            transform.localScale = Vector3.one;
        }
    }

    void HandleJumping()
    {
        if (coyoteTimeCounter > 0 && jumpBufferCounter > 0)
        {
            jumpBufferCounter = 0f;

            if (isOnGround)
            {
                canDoubleJump = true;
            }
            else
            {
                canDoubleJump = false;
            }

            // Jumping
            theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
        }

        if (Input.GetButtonUp("Jump") && theRB.velocity.y > 0f)
        {
            theRB.velocity = new Vector2(theRB.velocity.x, theRB.velocity.y * 0.5f);
            coyoteTimeCounter = 0f;
        }
    }
}
