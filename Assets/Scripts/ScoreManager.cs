using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {
    
    public static ScoreManager instance = null;

    private int score = 0;                      
    private int highScore = 0;
    private bool newHighScore = false;


    private void Awake()
    {
        //if we don't currently have a score manager set this one to be it and prevent destroying
        if (instance == null) instance = this;
        //if this score manager is not the current destroy this one because it is a duplicate
        else if (instance != this) Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        LeaderboardManager.instance.GetHighScoreAsync(GPGSIds.leaderboard_leaderboard, (int leaderboardScore) => {
            if (leaderboardScore > highScore) highScore = leaderboardScore;
        });
    }

    public int GetScore()
    {
        return score;
    }

    public int GetHighScore()
    {
        return highScore;
    }

    public bool IsNewHighScore()
    {
        return newHighScore;
    }

    public void IncrementScore() 
    {
        score++;
    }

    public void ResetScore() 
    {
        score = 0;
    }

    public void UpdateHighScore()
    {
        if (score > highScore) {
            newHighScore = true;
            highScore = score;
        }
        else newHighScore = false;

        LeaderboardManager.instance.PostScore(score, GPGSIds.leaderboard_leaderboard);

        // Try again to get high score from leaderboard (just in case it didn't work before)
        LeaderboardManager.instance.GetHighScoreAsync(GPGSIds.leaderboard_leaderboard, (int leaderboardScore) => {
            if (leaderboardScore > highScore) highScore = leaderboardScore;
        });
    }

}
