using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    private int _currentScore;
    
    public int CurrentScore => _currentScore;

    [Header("Score Highlight")]
    public int scoreHighlightRange = 50;

    public CharacterSoundController soundController;

    private int _lastScoreHighlight = 0;

    void Start()
    {
        _currentScore = 0;
        _lastScoreHighlight = 0;
    }

    public void IncreaseCurrenScore(int increment)
    {
        _currentScore += increment;

        if (_currentScore - _lastScoreHighlight > scoreHighlightRange)
        {
            soundController.PlayScoreHighlight();
            _lastScoreHighlight += scoreHighlightRange;
        }
    }

    public void FinishScoring()
    {
        if (CurrentScore > ScoreData.highScore)
        {
            ScoreData.highScore = CurrentScore;
        }
    }
}
