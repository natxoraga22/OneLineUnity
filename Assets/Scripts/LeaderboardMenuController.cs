using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LeaderboardMenuController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        LeaderboardManager.instance.ShowLeaderboard();
	}
	
}
