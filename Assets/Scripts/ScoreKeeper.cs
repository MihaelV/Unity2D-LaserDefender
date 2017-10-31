using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {

    public static int score=0;
    private Text myText;

    private void Start()
    {
       myText = GetComponent<Text>();
       reset();
    }


    public void Score(int points)
    {
        Debug.Log("points");
        score += points;
        myText.text = score.ToString();
    }


    public static void reset()
    {
        score = 0;       
    }
}
