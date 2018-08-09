using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LeaderboardMenuController : MonoBehaviour {

	private void Start() 
    {
        LeaderboardManager.instance.ShowLeaderboard(GPGSIds.leaderboard_leaderboard);
	}
	
}
