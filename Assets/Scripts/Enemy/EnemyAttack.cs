using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float playerDamage;
    public PlayerStats playerStats;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("PlayerStats"))
        {
            playerStats = other.GetComponent<PlayerStats>();
            playerStats.TakeDamage(playerDamage);

            SpecialAttack();
        }
    }

    public virtual void SpecialAttack()
    {

    }
}
