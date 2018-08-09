using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour {
    
	private void Start() 
    {
        LeaderboardManager.instance.Authenticate();
	}

    public void ShowLeaderboard() 
    {
        LeaderboardManager.instance.ShowLeaderboard(GPGSIds.leaderboard_leaderboard);
    }
	
}
