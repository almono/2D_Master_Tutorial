using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance; // static stays in memory until game is closed
    private Fader fader;
    private Door door;
    public List<Gem> gemsList;

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

        gemsList = new List<Gem>();
    }

    private void Start()
    {
        
    }

    public static void RegisterDoor(Door door)
    {
        if (instance == null)
        {
            return;
        }

        instance.door = door;
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

        instance.gemsList.Clear();
        instance.fader.RestartLevel();
    }

    public static void RegisterGem(Gem gem)
    {
        if(instance == null)
        {
            return;
        }

        if(!instance.gemsList.Contains(gem))
        {
            instance.gemsList.Add(gem);
        }
    }

    public static void RemoveGemFromList(Gem gem)
    {
        if (instance == null)
        {
            return;
        }

        if (instance.gemsList.Contains(gem))
        {
            instance.gemsList.Remove(gem);

            if(instance.gemsList.Count <= 0)
            {
                instance.door.UnlockDoor();
            }
        }
    }
}
