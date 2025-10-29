using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScore : MonoBehaviour
{
    static private TextMeshProUGUI _UI_TEXT;  //text is shared through all instances of highscore
    static private int _SCORE = 1000;  //private so that if the score/text changes, we know its fromt this class

    private TextMeshProUGUI txtCom;

    void Awake() {  //built in Unity method that happens immediately when this instance of HighScore class is first created (occurs before start)
        _UI_TEXT = GetComponent<TextMeshProUGUI>();
    }

    static public int SCORE {  //any class can access the value of SCORE without changing _SCORE (set is private)
        get {
            return _SCORE;
        }
        private set{
            _SCORE = value;
            if(_UI_TEXT != null) {  //make sure the text is not null before trying to reutrn/print it
                _UI_TEXT.text = "High Score: " + value.ToString("#,0");
            }
        }
    }

static public void TRY_SET_HIGH_SCORE(int scoreToTry) {  //other classes can send the high score and test it to see if it beats the last one
    if(scoreToTry <= SCORE) {
        return;
    }
    SCORE = scoreToTry;
}
}
