using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance; // static stays in memory until game is closed
    private Fader fader;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        
    }

    public static void RegisterFader(Fader fd)
    {
        if(instance == null)
        {
            return;
        }

        instance.fader = fd;
    }

    public static void LoadLevel(int index)
    {
        if(instance == null)
        {
            return;
        }

        instance.fader.SetLevel(index);
    }

    public static void RestartLevel()
    {
        if(instance == null)
        {
            return;
        }

        instance.fader.RestartLevel();
    }
}
