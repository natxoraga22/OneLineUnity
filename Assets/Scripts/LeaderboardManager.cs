using UnityEngine;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;


public class LeaderboardManager : MonoBehaviour {

    public static LeaderboardManager instance = null;


    private void Awake()
    {
        if (instance == null) {
            instance = this;
            PlayGamesPlatform.DebugLogEnabled = true;
            PlayGamesPlatform.Activate();
        }
        else if (instance != this) Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    public void Authenticate() 
    {
        if (!Social.Active.localUser.authenticated) {
            Social.Active.localUser.Authenticate((bool success) => {
                if (success) Debug.Log("Google Play Services - AUTHENTICATION SUCCESS");
                else Debug.Log("Google Play Services - AUTHENTICATION FAILED");
            });
        }
    }

    public void ShowLeaderboard(string leaderboardID) 
    {
        if (Social.Active.localUser.authenticated) {
            PlayGamesPlatform.Instance.ShowLeaderboardUI(leaderboardID);
        }
    }

    public delegate void GetHighScoreCallback(int highScore);

    public void GetHighScoreAsync(string leaderboardID, GetHighScoreCallback callback) 
    {
        ILeaderboard leaderboard = PlayGamesPlatform.Instance.CreateLeaderboard();
        leaderboard.id = leaderboardID;
        leaderboard.timeScope = TimeScope.AllTime;
        leaderboard.LoadScores((bool success) => {
            if (success) callback((int)leaderboard.localUserScore.value);
        });
    }
	
    public void PostScore(int score, string leaderboardID) 
    {
        if (Social.Active.localUser.authenticated) {
            Social.Active.ReportScore(score, leaderboardID, (bool success) => {
                if (success) Debug.Log("Google Play Services - REPORT SCORE (" + score + ") SUCCESS");
                else Debug.Log("Google Play Services - REPORT SCORE (" + score + ") FAILED");
            });
        }
    }

    public void OnApplicationQuit()
    {
        SignOut();
    }

    public void SignOut() 
    {
        if (Social.Active.localUser.authenticated) {
            PlayGamesPlatform.Instance.SignOut();
        }
    }

}
