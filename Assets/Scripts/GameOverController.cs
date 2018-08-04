using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour {

	public Text scoreText;
	public Text highScoreText;

	void Start () 
	{
		scoreText.text = "" + GameController.current.GetScore ();
		highScoreText.text = "" + GameController.current.GetHighScore ();
	}

}
