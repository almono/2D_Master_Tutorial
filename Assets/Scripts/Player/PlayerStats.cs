using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float maxHealth = 100f, health;
    public bool canBeDamaged = true;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        if(canBeDamaged)
        {
            health -= damage;
            // play hurt animation

            if (health <= 0)
            {
                Debug.Log("DEATH");
                GetComponent<PolygonCollider2D>().enabled = false;
                GetComponentInParent<GatherInput>().DisableControls();
            }

            StartCoroutine(DamagePrevention());
        }
    }

    public IEnumerator DamagePrevention()
    {
        canBeDamaged = false;
        yield return new WaitForSeconds(0.2f);

        if(health > 0)
        {
            canBeDamaged = true;
        } else
        {
            // play death animation
        }
    }
}
