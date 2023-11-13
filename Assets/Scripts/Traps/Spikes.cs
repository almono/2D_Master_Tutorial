using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    public float damage = 10f;
    public float forceX = 8f, forceY = 5f, duration = 0.15f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        other.GetComponent<PlayerStats>().TakeDamage(damage);
        PlayerMoveControls playerMove = other.GetComponentInParent<PlayerMoveControls>();

        StartCoroutine(playerMove.KnockbackPlayer(forceX, forceY, duration, transform));
    }
}
