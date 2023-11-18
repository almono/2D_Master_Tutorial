using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    public GameObject gemParticles;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.RegisterGem(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            //Destroy(gameObject);

            other.GetComponent<PlayerCollectibles>().GemCollected();

            // More performance heavy
            GetComponent<AudioSource>().Play();
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<CircleCollider2D>().enabled = false;

            Instantiate(gemParticles, transform.position, transform.rotation);

            GameManager.RemoveGemFromList(this);
        }
    }
}
