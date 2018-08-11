using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseGameButton : MonoBehaviour {

	public Sprite playWhiteSprite;
    public Sprite playBlackSprite;
	public Sprite pauseWhiteSprite;
    public Sprite pauseBlackSprite;

	private Image image;
	private bool gamePaused = false;
    private Color color = Color.white;

	void Start()
	{
		image = GetComponent<Image> ();
	}

	public void Pause()
	{
		gamePaused = !gamePaused;
        UpdateSprite();
        AudioManager.instance.SetMute(gamePaused);
		if (gamePaused) GameController.current.PauseGame();
		else GameController.current.ResumeGame();
	}

    public void SetColor(Color newColor) 
    {
        color = newColor;
        UpdateSprite();
    }

    private void UpdateSprite() 
    {
        if (gamePaused) {
            if (color == Color.white) image.sprite = playWhiteSprite;
            else if (color == Color.black) image.sprite = playBlackSprite;
        }
        else {
            if (color == Color.white) image.sprite = pauseWhiteSprite;
            else if (color == Color.black) image.sprite = pauseBlackSprite;
        }

    }

}
