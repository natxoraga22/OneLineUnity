using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;


public class GameOverController : MonoBehaviour {

    public AdsManager adsManager;
	public Text scoreText;
	public Text highScoreText;

    public int gameOversBetweenAds = 5;
    private static int gameOversSinceAd = 0;


	void Start () 
	{
        //update UI
        if (ScoreManager.instance) {
            scoreText.text = "" + ScoreManager.instance.GetScore();
            highScoreText.text = "" + ScoreManager.instance.GetHighScore();
            highScoreText.gameObject.GetComponentInParent<Animator>().enabled = ScoreManager.instance.IsNewHighScore();
        }

        //show ad if needed
        gameOversSinceAd++;
        if (gameOversSinceAd >= gameOversBetweenAds && Advertisement.IsReady()) {
            gameOversSinceAd = 0;
            Advertisement.Show();
        }
	}

}
