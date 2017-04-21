using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour {

	public Sprite playSprite;
	public Sprite pauseSprite;

	private Image image;
	private bool gamePaused = false;

	void Start()
	{
		image = GetComponent<Image> ();
	}

	public void Pause()
	{
		gamePaused = !gamePaused;
		if (gamePaused) {
			image.sprite = playSprite;
			GameController.current.PauseGame ();
		} 
		else {
			image.sprite = pauseSprite;
			GameController.current.ResumeGame ();
		}
	}
}
