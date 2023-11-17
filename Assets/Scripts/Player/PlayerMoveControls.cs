using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerMoveControls : MonoBehaviour
{
    public float speed = 4f, jumpForce;
    private int isFacingRight = -1;

    private GatherInput gI;
    private Rigidbody2D rB;
    private Animator playerAnim;

    public float rayLength = 0.05f;
    public LayerMask groundLayer;
    public Transform leftPoint, rightPoint;
    private bool grounded = true;
    public bool isKnockbacked = false;
    public bool hasControl = true;

    public int additionalJumps = 3;
    private int resetJumpsNumber;

    // Start is called before the first frame update
    void Start()
    {
        gI = GetComponent<GatherInput>();
        rB = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();

        resetJumpsNumber = additionalJumps;
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
            Jump();
        }
    }

    void Move()
    {
        Flip();
        rB.velocity = new Vector2(speed * gI.valueX, rB.velocity.y);
    }

    void Jump()
    {
        if (gI.jumpInput)
        {
            if (grounded)
            {
                rB.velocity = new Vector2(gI.valueX * speed, jumpForce);
            } else if (additionalJumps > 0)
            {
                rB.velocity = new Vector2(gI.valueX * speed, jumpForce * 0.6f);
                additionalJumps--;
            }

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

    private void SetAnimatorValues()
    {
        playerAnim.SetFloat("speed", Mathf.Abs(rB.velocity.x));
        playerAnim.SetFloat("vspeed", rB.velocity.y);
        playerAnim.SetBool("grounded", grounded);
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

        SeeRays(leftCheckHit, rightCheckHit);
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
