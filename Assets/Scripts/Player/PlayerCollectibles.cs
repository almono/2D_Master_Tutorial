using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCollectibles : MonoBehaviour
{

    public int gemCount;
    public Text gemCountText;

    // Start is called before the first frame update
    void Start()
    {
        // for prefabs we can do this so we dont have to always set the object references
        //gemCountText = GameObject.FindGameObjectWithTag("GemUI").GetComponentInChildren<Text>();
        UpdateText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateText()
    {
        gemCountText.text = gemCount.ToString();
    }

    public void GemCollected()
    {
        gemCount++;
        UpdateText();
    }
}
