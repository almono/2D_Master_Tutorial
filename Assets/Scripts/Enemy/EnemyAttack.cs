using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float playerDamage;
    public PlayerStats playerStats;
    private CinemachineImpulseSource impulseSource;

    private void Start()
    {
        impulseSource = GetComponentInParent<CinemachineImpulseSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("PlayerStats"))
        {
            playerStats = other.GetComponent<PlayerStats>();
            playerStats.TakeDamage(playerDamage);
            impulseSource.GenerateImpulse(); // camera shake effect on getting hit

            SpecialAttack();
        }
    }

    public virtual void SpecialAttack()
    {

    }
}
