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
    public AudioSource audioSource;

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
            // must have controls enabled
            // cant be in knockback state
            // cant be on ladder
            if(attackStarted || playerMoveControls.hasControl == false || playerMoveControls.isKnockbacked || playerMoveControls.onLadder)
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
        audioSource.Play();
    }

    public void ResetAttack()
    {
        // reset attack bool
        playerAnim.SetBool("attack", false);
        gI.tryAttack = false;
        attackStarted = false;
        meleeAttackCollider.enabled = false;
        audioSource.Stop();
    }
}
