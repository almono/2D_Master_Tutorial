using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackControls : MonoBehaviour
{
    private PlayerMoveControls playerMoveControls;
    private GatherInput gI;
    private Animator playerAnim;

    public bool attackStarted = false;
    public PolygonCollider2D meleeAttackCollider;

    // Start is called before the first frame update
    void Start()
    {
        playerMoveControls = GetComponent<PlayerMoveControls>();
        gI = GetComponent<GatherInput>();
        playerAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    public void Attack()
    {
        if (gI.tryAttack)
        {
            // check if we are already attacking
            if(attackStarted || playerMoveControls.hasControl == false || playerMoveControls.isKnockbacked)
            {
                return;
            }

            playerAnim.SetBool("attack", true);
            attackStarted = true;
        }
    }

    public void ActivateAttack()
    {
        meleeAttackCollider.enabled = true;
    }

    public void ResetAttack()
    {
        // reset attack bool
        playerAnim.SetBool("attack", false);
        gI.tryAttack = false;
        attackStarted = false;
        meleeAttackCollider.enabled = false;
    }
}
