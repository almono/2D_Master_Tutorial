using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    float attackDamage = 50f;
    private int enemyLayer;
    public int destructibleItemLayer;

    // Start is called before the first frame update
    void Start()
    {
        enemyLayer = LayerMask.NameToLayer("Enemy");
        destructibleItemLayer = LayerMask.NameToLayer("Destructible");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == enemyLayer)
        {
            other.GetComponent<Enemy>().TakeDamage(attackDamage);
        }

        if (other.gameObject.layer == destructibleItemLayer)
        {
            other.GetComponent<DestructibleItems>().HitDestructible();
        }
    }
}
