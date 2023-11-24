using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerMoveControls : MonoBehaviour
{
    public float speed = 4f, jumpForce, jumpTimer = 0.2f; // jump timer is time between jumps
    private int isFacingRight = -1;

    private GatherInput gI;
    private Rigidbody2D rB;
    private Animator playerAnim;

    public float rayLength = 0.05f;
    public LayerMask groundLayer;
    public Transform leftPoint, rightPoint;

    [SerializeField]  private bool grounded = true;
    public bool isKnockbacked = false;
    public bool hasControl = true;

    [Header("Climbing")]
    public bool onLadder = false;
    public float climbSpeed = 3f;
    public float climbHorizontalSpeed = 1f;
    private float startGravity;

    [Header("Extra Jumps")]
    public int additionalJumps = 3;
    private int resetJumpsNumber;

    [Header("Wall slide")]
    public Transform wallSlidePoint;
    public float wallSlideSpeed = 2f;
    [SerializeField]  private bool isWallSliding = false;

    [Header("Walljump")]
    [SerializeField] private bool wallJumpActive;
    private bool wallJumpOneTime;
    public float wallJumpTimer = 0.2f;
    private float startWallJumpTimer;
    private int wallJumpDirection;
    public float wallJumpForceX = 8f, wallJumpForceY = 8f;
    RaycastHit2D wallSlideHit;

    // Start is called before the first frame update
    void Start()
    {
        gI = GetComponent<GatherInput>();
        rB = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
        startGravity = rB.gravityScale;

        resetJumpsNumber = additionalJumps;
        startWallJumpTimer = wallJumpTimer;
    }

    // Update is called once per frame
    void Update()
    {
        SetAnimatorValues();
    }

    void FixedUpdate()
    {
        CheckStatus();

        if(!isKnockbacked && hasControl)
        {
            Move();
            WallSlide();
            Jump();
            WallJump();
        }
    }

    void Move()
    {
        // restrict movement is wall jump is active
        if(wallJumpActive)
        {
            return;
        }

        Flip();
        rB.velocity = new Vector2(speed * gI.valueX, rB.velocity.y);

        if(onLadder)
        {
            rB.gravityScale = 0f;
            rB.velocity = new Vector2(climbHorizontalSpeed * gI.valueX, climbSpeed * gI.valueY);

            if(rB.velocity.y == 0)
            {
                playerAnim.enabled = false; // stop climbing animation if not moving on ladder
            } else
            {
                playerAnim.enabled = true;
            }
        }
    }

    public void ExitLadder()
    {
        rB.gravityScale = startGravity;
        onLadder = false;
        playerAnim.enabled = true;
    }

    void Jump()
    {
        if (gI.jumpInput && jumpTimer <= 0f)
        {
            if (grounded || onLadder)
            {
                jumpTimer = 0.2f;
                ExitLadder();
                rB.velocity = new Vector2(gI.valueX * speed, jumpForce);
            } else if (additionalJumps > 0 && !wallSlideHit)
            {
                additionalJumps--;
                jumpTimer = 0.2f;
                ExitLadder();
                rB.velocity = new Vector2(gI.valueX * speed, jumpForce * 0.8f);
            }
            // wall jumping
            else if(!wallJumpOneTime && wallSlideHit)
            {
                wallJumpActive = true; 
                wallJumpOneTime = true;
                wallJumpDirection = (-1 * isFacingRight);
                wallJumpTimer = startWallJumpTimer;
                ForceFlip();
            }
        } else
        {
            wallJumpOneTime = false;
        }

        if (jumpTimer > 0)
        {
            jumpTimer -= Time.deltaTime;
        }

        if(wallJumpActive == true && wallJumpTimer >= 0) 
        { 
            wallJumpTimer -= Time.deltaTime;
        } else if (wallJumpTimer <= 0)
        {
            wallJumpActive = false;
        }

        gI.jumpInput = false; // no need to wait for player to stop pressing spacebar
    }

    void Flip()
    {
        if (gI.valueX * isFacingRight > 0)
        {
            transform.localScale = new Vector3(-transform.localScale.x, 1, 1);
            isFacingRight *= -1;
        }
    }

    void ForceFlip()
    {
        transform.localScale = new Vector3(-transform.localScale.x, 1, 1);
        isFacingRight *= -1;
    }

    private void SetAnimatorValues()
    {
        playerAnim.SetFloat("speed", Mathf.Abs(rB.velocity.x));
        playerAnim.SetFloat("vspeed", rB.velocity.y);
        playerAnim.SetBool("grounded", grounded);
        playerAnim.SetBool("isClimbing", onLadder);
        //playerAnim.SetBool("isWallSliding", isWallSliding);
    }

    private void CheckStatus()
    {
        RaycastHit2D leftCheckHit = Physics2D.Raycast(leftPoint.position, Vector2.down, rayLength, groundLayer);
        RaycastHit2D rightCheckHit = Physics2D.Raycast(rightPoint.position, Vector2.down, rayLength, groundLayer);

        if (leftCheckHit || rightCheckHit)
        {
            grounded = true;
            additionalJumps = resetJumpsNumber;
        } else
        {
            grounded = false;
        }

        wallSlideHit = Physics2D.Raycast(wallSlidePoint.position, isFacingRight * Vector2.right, 0.1f, groundLayer);

        if(wallSlideHit && !grounded)
        {
            if(gI.tryToWalSlide)
            {
                isWallSliding = true;
            } else
            {
                isWallSliding = false;
            }
        } else
        {
            isWallSliding = false;
        }

        SeeRays(leftCheckHit, rightCheckHit);
    }

    private void WallSlide()
    {
        if(isWallSliding)
        {
            rB.velocity = new Vector2(rB.velocity.x, Mathf.Clamp(rB.velocity.y, -wallSlideSpeed, 5));
        }
    }

    private void WallJump()
    {
        if (wallJumpActive)
        {
            rB.velocity = new Vector2(-wallJumpDirection * wallJumpForceX, wallJumpForceY);
            Debug.Log("TEST");
        }
    }

    private void SeeRays(RaycastHit2D leftCheckHit, RaycastHit2D rightCheckHit)
    {
        Color color1 = leftCheckHit ? Color.red : Color.green;
        Color color2 = rightCheckHit ? Color.red : Color.green;

        Debug.DrawRay(leftPoint.position, Vector2.down * rayLength, color1);
        Debug.DrawRay(rightPoint.position, Vector2.down * rayLength, color2);
    }

    public IEnumerator KnockbackPlayer(float forceX, float forceY, float duration, Transform otherObject)
    {
        int knockbackDirection = (transform.position.x < otherObject.position.x) ? -1 : 1;

        isKnockbacked = true;

        // reset all forces/velocities/movements on player
        rB.velocity = Vector2.zero;

        Vector2 knockbackForce = new Vector2(knockbackDirection * forceX, forceY);
        rB.AddForce(knockbackForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(duration);

        isKnockbacked = false;
        rB.velocity = Vector2.zero; // make sure we reset forces back after knockback ends
    }
}
