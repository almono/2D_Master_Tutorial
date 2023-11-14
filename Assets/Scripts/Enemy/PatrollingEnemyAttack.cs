using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingEnemyAttack : EnemyAttack
{
    PlayerMoveControls playerMove;
    public float knockbackForceX = 12f, knockbackForceY = 3f, knockbackDuration = 0.3f;
    public override void SpecialAttack()
    {
        // calls all code from parent
        //base.SpecialAttack();
        playerMove = playerStats.GetComponentInParent<PlayerMoveControls>();

        // we need to give parent position as the object position
        StartCoroutine(playerMove.KnockbackPlayer(knockbackForceX, knockbackForceY, knockbackDuration, transform.parent));

    }
}
