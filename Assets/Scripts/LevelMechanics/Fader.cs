using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fader : MonoBehaviour
{
    private Animator fadeAnim;
    private int levelToLoad;

    // Start is called before the first frame update
    void Start()
    {
        fadeAnim = GetComponent<Animator>();

        GameManager.RegisterFader(this);
    }

    // Update is called once per frame
    public void SetLevel(int lvl)
    {
        levelToLoad = lvl;
        fadeAnim.SetTrigger("fade");
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(levelToLoad);
    }

    private void Restart()
    {
        SetLevel(SceneManager.GetActiveScene().buildIndex);
    }

    public void RestartLevel()
    {
        Invoke("Restart", 1.5f);
    }
}
