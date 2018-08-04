using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour {
    
	public Text scoreText;
	public Text highScoreText;
    public Text newHighScoreText;

	void Start () 
	{
        if (ScoreManager.instance) {
            scoreText.text = "" + ScoreManager.instance.GetScore();
            highScoreText.text = "" + ScoreManager.instance.GetHighScore();
            Color newHighTextScoreColor = new Color(1f, 1f, 1f, ScoreManager.instance.IsNewHighScore() ? 1f : 0f);
            newHighScoreText.color = newHighTextScoreColor;
        }
	}

}
