using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartHeal : MonoBehaviour
{
    public float healAmount = 15f;

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
        if(other.CompareTag("Player"))
        {
            other.GetComponentInChildren<PlayerStats>().HealPlayer(healAmount);
            GetComponent<AudioSource>().Play();
            //Destroy(gameObject);

            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<CircleCollider2D>().enabled = false;
        }
    }
}
