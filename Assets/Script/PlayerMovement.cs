using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool canMove = true;
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

    float originalGravityScale;

    [SerializeField] Animator fullBodyAnimator;
    [SerializeField] Animator emptyBodyAnimator;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
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
                coyoteTimeCounter = coyoteTime;
            }
            else
            {
                coyoteTimeCounter -= Time.deltaTime;
            }

            HandleJumping();
        }
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

        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            fullBodyAnimator.SetBool("isMoving", true);
            emptyBodyAnimator.SetBool("isMoving", true);
        }
        else
        {
            fullBodyAnimator.SetBool("isMoving", false);
            emptyBodyAnimator.SetBool("isMoving", false);
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

            StartCoroutine(HandleJumpAnimation());

            // Jumping
            theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
        }

        if (Input.GetButtonUp("Jump") && theRB.velocity.y > 0f)
        {
            theRB.velocity = new Vector2(theRB.velocity.x, theRB.velocity.y * 0.5f);
            coyoteTimeCounter = 0f;
        }
    }

    IEnumerator HandleJumpAnimation()
    {
        emptyBodyAnimator.SetBool("isJumping", true);
        fullBodyAnimator.SetBool("isJumping", true);
        yield return new WaitForSeconds(0.1f);
        emptyBodyAnimator.SetBool("isJumping", false);
        fullBodyAnimator.SetBool("isJumping", false);
    }

    public void DoHangTime()
    {
        canMove = false;
        theRB.velocity = Vector2.zero;
        originalGravityScale = theRB.gravityScale;
        theRB.gravityScale = 0f;
        theRB.bodyType = RigidbodyType2D.Kinematic;
        theRB.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
    }

    public void StopHangTime()
    {
        canMove = true;
        theRB.gravityScale = originalGravityScale;
        theRB.bodyType = RigidbodyType2D.Dynamic;
        theRB.constraints = RigidbodyConstraints2D.None;
        theRB.constraints = RigidbodyConstraints2D.FreezeRotation;

    }
}
