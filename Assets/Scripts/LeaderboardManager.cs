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

    public void OnApplicationQuit()
    {
        SignOut();
    }

    public void Authenticate() 
    {
        if (!Social.localUser.authenticated) {
            Social.localUser.Authenticate((bool success) => {
                if (success) Debug.Log("Google Play Services - AUTHENTICATION SUCCESS");
                else Debug.Log("Google Play Services - AUTHENTICATION FAILED");
            });
        }
    }

    public void ShowLeaderboard(string leaderboardID) 
    {
        if (Social.localUser.authenticated) {
            PlayGamesPlatform.Instance.ShowLeaderboardUI(leaderboardID);
        }
    }

    public int GetHighScore(string leaderboardID) 
    {
        int highScore = 0;
        if (Social.localUser.authenticated) {
            Social.LoadScores(leaderboardID, (IScore[] scores) => {
                foreach (IScore score in scores) {
                    if (score.userID == Social.localUser.id) highScore = (int)score.value;
                }
            });
        }
        return highScore;
    }
	
    public void PostScore(int score, string leaderboardID) 
    {
        if (Social.localUser.authenticated) {
            Social.ReportScore(score, leaderboardID, (bool success) => {
                if (success) Debug.Log("Google Play Services - REPORT SCORE (" + score + ") SUCCESS");
                else Debug.Log("Google Play Services - REPORT SCORE (" + score + ") FAILED");
            });
        }
    }

    public void SignOut() 
    {
        if (Social.localUser.authenticated) {
            PlayGamesPlatform.Instance.SignOut();
        }
    }

}
