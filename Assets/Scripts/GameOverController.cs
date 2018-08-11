using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;


public class GameOverController : MonoBehaviour {

    public AdsManager adsManager;
	public Text scoreText;
	public Text highScoreText;
    public Text newHighScoreText;

    public int gameOversBetweenAds = 5;
    private static int gameOverCounter = 0;


	void Start () 
	{
        //update UI
        if (ScoreManager.instance) {
            scoreText.text = "" + ScoreManager.instance.GetScore();
            highScoreText.text = "" + ScoreManager.instance.GetHighScore();
            Color newHighTextScoreColor = new Color(1f, 1f, 1f, ScoreManager.instance.IsNewHighScore() ? 1f : 0f);
            newHighScoreText.color = newHighTextScoreColor;
        }

        //show ad if needed
        gameOverCounter++;
        if (gameOverCounter >= gameOversBetweenAds && Advertisement.IsReady()) {
            gameOverCounter = 0;
            Advertisement.Show();
        }
	}

}
