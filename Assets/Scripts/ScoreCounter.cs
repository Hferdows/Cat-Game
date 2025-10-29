using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCounter : MonoBehaviour {
    [Header ("Dynamic")]   //means the score field will change during the game 
    public int     score = 0;
    
    private TextMeshProUGUI uiText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        uiText = GetComponent<TextMeshProUGUI>();  //gets the text from the gameobject and stores it here, for fast access later 

    }

    // Update is called once per frame
    void Update()
    {
        uiText.text = "SCORE: " + score.ToString("#,0"); 
        
    }

}
