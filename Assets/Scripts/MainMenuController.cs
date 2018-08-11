using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MainMenuController : MonoBehaviour {
    
    public Button muteButton;

    public Sprite audioOnSprite;
    public Sprite audioOffSprite;


	private void Start() 
    {
        LeaderboardManager.instance.Authenticate();

        if (AudioManager.instance.IsMuted()) muteButton.image.sprite = audioOffSprite;
        else muteButton.image.sprite = audioOnSprite;
	}

    public void ShowLeaderboard() 
    {
        LeaderboardManager.instance.ShowLeaderboard(GPGSIds.leaderboard_leaderboard);
    }

    public void ToggleMute() 
    {
        AudioManager.instance.ToggleMute();
        if (AudioManager.instance.IsMuted()) muteButton.image.sprite = audioOffSprite;
        else muteButton.image.sprite = audioOnSprite;
    }
	
}
