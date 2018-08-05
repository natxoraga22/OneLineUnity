using UnityEngine;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;


public class LeaderboardManager : MonoBehaviour {

    public static LeaderboardManager instance = null;


    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
        DontDestroyOnLoad(gameObject); 
    }

    public void Start()
    {
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
    }

    public void Authenticate() 
    {
        if (!Social.localUser.authenticated) {
            Social.localUser.Authenticate((bool success) => {
                if (success) Debug.Log("AUTHENTICATION SUCCESS");
                else Debug.Log("AUTHENTICATION FAILED");
            });
        }
    }

    public void ShowLeaderboard() 
    {
        Social.ShowLeaderboardUI();
    }
	
    public void PostScore(int score) 
    {
        if (Social.localUser.authenticated) {
            Social.ReportScore(score, GPGSIds.leaderboard_leaderboard, (bool success) => {
                Debug.Log("REPORT SCORE");
            });
        }
    }

}
