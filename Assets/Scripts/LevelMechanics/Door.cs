using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public int levelToLoad;
    public Sprite openedDoors;
    private BoxCollider2D doorCollider;

    private void Start()
    {
        doorCollider = GetComponent<BoxCollider2D>();
        GameManager.RegisterDoor(this);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            // disable to ensure its called once
            doorCollider.enabled = false;
            other.GetComponent<GatherInput>().DisableControls();

            // save player health for next level
            PlayerStats playerStats = other.GetComponentInChildren<PlayerStats>();
            PlayerPrefs.SetFloat("Player_Health", playerStats.health);

            //fader.SetLevel(levelToLoad);
            GameManager.LoadLevel(levelToLoad);
            PlayerPrefs.SetInt("Saved_Level", levelToLoad);
        }
    }

    public void UnlockDoor()
    {
        GetComponent<SpriteRenderer>().sprite = openedDoors;
        doorCollider.enabled = true;
    }
}
