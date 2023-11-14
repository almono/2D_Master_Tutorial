using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public int levelToLoad;
    //public string levelNameToLoad;

    public void LoadLevel()
    {
        SceneManager.LoadScene(levelToLoad);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            // disable to ensure its called once
            GetComponent<BoxCollider2D>().enabled = false;
            other.GetComponent<GatherInput>().DisableControls();

            LoadLevel();
        }
    }
}
