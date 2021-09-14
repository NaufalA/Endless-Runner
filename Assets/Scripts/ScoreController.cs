using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    private int _currentScore;
    
    public int CurrentScore => _currentScore;

    void Start()
    {
        _currentScore = 0;
    }

    public void IncreaseCurrenScore(int increment)
    {
        _currentScore += increment;
    }

    public void FinishScoring()
    {
        if (CurrentScore > ScoreData.highScore)
        {
            ScoreData.highScore = CurrentScore;
        }
    }
}
