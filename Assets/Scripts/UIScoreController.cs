using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScoreController : MonoBehaviour
{
    [Header("UI")]
    public Text score;
    public Text highScore;

    [Header("Scoring")]
    public ScoreController scoreController;
    
    private void Update()
    {
        score.text = scoreController.CurrentScore.ToString();
        highScore.text = ScoreData.highScore.ToString();
    }
}
