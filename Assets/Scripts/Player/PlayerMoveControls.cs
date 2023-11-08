using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerMoveControls : MonoBehaviour
{
    public float speed = 4f;
    private int isFacingRight = -1;

    private GatherInput gI;
    private Rigidbody2D rB;
    private Animator playerAnim;

    // Start is called before the first frame update
    void Start()
    {
        gI = GetComponent<GatherInput>();
        rB = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        SetAnimatorValues();
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        Flip();
        rB.velocity = new Vector2(speed * gI.valueX, rB.velocity.y);
    }

    void Flip()
    {
        if(gI.valueX * isFacingRight > 0)
        {
            transform.localScale = new Vector3(-transform.localScale.x, 1, 1);
            isFacingRight *= -1;
        }
    }

    private void SetAnimatorValues()
    {
        playerAnim.SetFloat("speed", Mathf.Abs(rB.velocity.x));
    }
}
