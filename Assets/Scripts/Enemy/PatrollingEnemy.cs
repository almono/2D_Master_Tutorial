using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingEnemy : Enemy
{
    public float speed = 2f;
    private int direction = -1;

    public Transform groundCheck;
    public Transform wallCheck;
    public LayerMask layerToCheck;

    private bool detectGround, detectWall;

    public float checkRadius;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Flip();
        enemyBody.velocity = new Vector2(direction * speed, enemyBody.velocity.y);
    }

    void Flip()
    {
        detectGround = Physics2D.OverlapCircle(groundCheck.position, checkRadius, layerToCheck);
        detectWall = Physics2D.OverlapCircle(wallCheck.position, checkRadius, layerToCheck);

        if(detectWall || detectGround == false)
        {
            direction *= -1;
            transform.localScale = new Vector3(-transform.localScale.x, 1, 1);
        }
    }

    public override void HurtSequence()
    {
        enemyAnim.SetTrigger("hurt");
    }

    public override void DeathSequence()
    {
        enemyAnim.SetTrigger("death");
        speed = 0f;
        GetComponent<CapsuleCollider2D>().enabled = false;
        GetComponentInChildren<PolygonCollider2D>().enabled = false;
        enemyBody.gravityScale = 0f;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
        Gizmos.DrawWireSphere(wallCheck.position, checkRadius);
    }
}
