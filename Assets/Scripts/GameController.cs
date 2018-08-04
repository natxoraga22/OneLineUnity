using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public static GameController current;		//a reference to our game controller so we can access it statically

	// Game components
	public Text scoreText;
	public Button playPauseButton;
	public Camera mainCamera;
	public PlayerController playerController;	
	public WallSpawner wallSpawner;
    public GameObject rightLimit;
    public GameObject leftLimit;

	private Color currentElementsColor = Color.white;
	private Color currentBackgroundColor = Color.black;

	private int score = 0;						//the player's score
	private static int highScore = 0;

	void Awake()
	{
		//if we don't currently have a game control set this one to be it
		if (current == null) current = this;
		//if this game controller is not the current destroy this one because it is a duplicate
		else if (current != this) Destroy (gameObject);
	}

    private void Start()
    {
        //position right and left limits depending on screen width
        float screenMinX = mainCamera.ScreenToWorldPoint(new Vector3(0f, 0f, 0f)).x;
        float screenMaxX = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, 0f, 0f)).x;
        leftLimit.transform.position = new Vector3(screenMinX - leftLimit.GetComponent<BoxCollider2D>().size.x / 2f, leftLimit.transform.position.y, leftLimit.transform.position.z);
        rightLimit.transform.position = new Vector3(screenMaxX + rightLimit.GetComponent<BoxCollider2D>().size.x / 2f, rightLimit.transform.position.y, rightLimit.transform.position.z);
    }

    public int GetScore()
	{
		return score;
	}

	public int GetHighScore()
	{
		return highScore;
	}

	public void PlayerScored()
	{
		// Each time the player scores, change background, player and walls color
		if (currentElementsColor == Color.white) {
			currentElementsColor = Color.black;
			currentBackgroundColor = Color.white;
		} 
		else if (currentElementsColor == Color.black) {
			currentElementsColor = Color.white;
			currentBackgroundColor = Color.black;
		}
		mainCamera.backgroundColor = currentBackgroundColor;
		scoreText.color = currentElementsColor;
		playPauseButton.image.color = currentElementsColor;
		playerController.SetColor (currentElementsColor);
		wallSpawner.SetColor (currentElementsColor);

		// Increment the player's speed
		playerController.IncrementSpeed ();

		score++;
		scoreText.text = "" + score;
	}

	public void PlayerDied()
	{
		// Stop wall spawning
		wallSpawner.StopSpawn ();

		// Update the high score
		if (score > highScore) highScore = score;

		// Load GameOver scene
		StartCoroutine(LoadSceneAfterDelay(2, 1.5f));
	}

	IEnumerator LoadSceneAfterDelay(int scene, float seconds)
	{
		yield return new WaitForSeconds (seconds);
		SceneManager.LoadScene (scene);
	}

	public void PauseGame()
	{
		Time.timeScale = 0f;
	}

	public void ResumeGame()
	{
		Time.timeScale = 1f;
	}

}
