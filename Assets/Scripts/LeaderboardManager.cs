using UnityEngine;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;


public class LeaderboardManager : MonoBehaviour {

    public static LeaderboardManager instance = null;


    private void Awake()
    {
        Debug.Log("LeaderboardManager.Awake()");
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
        Debug.Log("LeaderboardManager.Authenticate()");
        if (!Social.localUser.authenticated) {
            Debug.Log("Not authenticated. Trying to");
            Social.localUser.Authenticate((bool success) => {
                if (success) Debug.Log("AUTHENTICATION SUCCESS");
                else Debug.Log("AUTHENTICATION FAILED");
            });
            /*PlayGamesPlatform.Instance.Authenticate((bool success) => {
                if (success) Debug.Log("AUTHENTICATION SUCCESS");
                else Debug.Log("AUTHENTICATION FAILED");
            });*/
        }
    }

    public void ShowLeaderboard() 
    {
        Debug.Log("LeaderboardManager.ShowLeaderboard()");
        Social.ShowLeaderboardUI();
    }
	
    public void PostScore(int score) 
    {
        Debug.Log("LeaderboardManager.PostScore(" + score + ")");
        if (Social.localUser.authenticated) {
            Social.ReportScore(score, GPGSIds.leaderboard_leaderboard, (bool success) => {
                Debug.Log("REPORT SCORE " + score);
            });
        }
    }

}
