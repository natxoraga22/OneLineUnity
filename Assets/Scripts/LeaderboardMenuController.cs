using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LeaderboardMenuController : MonoBehaviour {

	// Use this for initialization
	private void Start() 
    {
        LeaderboardManager.instance.ShowLeaderboard(GPGSIds.leaderboard_leaderboard);
	}
	
}
