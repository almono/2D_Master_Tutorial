using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayButton : MonoBehaviour
{
    public TextMeshProUGUI buttonText;

    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.HasKey("Saved_Level"))
        {
            buttonText.text = "Continue";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
