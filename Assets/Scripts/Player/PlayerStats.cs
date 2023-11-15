using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public float maxHealth = 100f, health;
    public bool canBeDamaged = true;

    public Animator playerAnim;
    private PlayerMoveControls playerMove;

    public Image healthUI;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        playerMove = GetComponentInParent<PlayerMoveControls>();
        playerAnim = GetComponentInParent<Animator>();
        UpdateHealthUI();
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
            playerAnim.SetBool("damaged", true);
            playerMove.hasControl = false;

            UpdateHealthUI();

            if (health <= 0)
            {
                GetComponent<PolygonCollider2D>().enabled = false;
                GetComponentInParent<GatherInput>().DisableControls();
                GameManager.RestartLevel();
            }

            StartCoroutine(DamagePrevention());
        }
    }

    public void HealPlayer(float healAmount)
    {
        if(health + healAmount > maxHealth)
        {
            health = maxHealth;
        } else
        {
            health += healAmount;
        }

        UpdateHealthUI();
    }


    public IEnumerator DamagePrevention()
    {
        canBeDamaged = false;
        yield return new WaitForSeconds(0.2f);

        if(health > 0)
        {
            canBeDamaged = true;
            playerMove.hasControl = true;
            playerAnim.SetBool("damaged", false);
        } else
        {
            playerAnim.SetBool("death", true);
        }
    }

    public void UpdateHealthUI()
    {
        healthUI.fillAmount = health / maxHealth;
    }
}
