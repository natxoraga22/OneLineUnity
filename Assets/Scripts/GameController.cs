﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public static GameController current;		//a reference to our game controller so we can access it statically

	// Game components
    public PauseGameButton playPauseButton;
	public Camera mainCamera;
	public PlayerController playerController;	
	public WallSpawner wallSpawner;
    public Text scoreText;
    public Image scoreTextBackground;
    public GameObject rightLimit;
    public GameObject leftLimit;
    public Canvas tutorial;

	private Color currentElementsColor = Color.white;
	private Color currentBackgroundColor = Color.black;


	private void Awake()
	{
		//if we don't currently have a game control set this one to be it
		if (current == null) current = this;
		//if this game controller is not the current destroy this one because it is a duplicate
		else if (current != this) Destroy (gameObject);
	}

    private void Start()
    {
        //set score to 0
        ScoreManager.instance.ResetScore();

        //position right and left limits depending on screen width
        float screenMinX = mainCamera.ScreenToWorldPoint(new Vector3(0f, 0f, 0f)).x;
        float screenMaxX = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, 0f, 0f)).x;
        leftLimit.transform.position = new Vector3(screenMinX - leftLimit.GetComponent<BoxCollider2D>().size.x / 2f, leftLimit.transform.position.y, leftLimit.transform.position.z);
        rightLimit.transform.position = new Vector3(screenMaxX + rightLimit.GetComponent<BoxCollider2D>().size.x / 2f, rightLimit.transform.position.y, rightLimit.transform.position.z);

        //show canvas if first time playing
        if (!PlayerPrefs.HasKey("tutorialCompleted")) {
            PauseGame();
            tutorial.gameObject.SetActive(true);
        }
    }

    public void TutorialCompleted()
    {
        PlayerPrefs.SetString("tutorialCompleted", "true");
        PlayerPrefs.Save();
        tutorial.gameObject.SetActive(false);
        ResumeGame();
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
        scoreText.color = currentBackgroundColor;
        scoreTextBackground.color = currentElementsColor;
        playPauseButton.SetColor(currentElementsColor);
		playerController.SetColor (currentElementsColor);
		wallSpawner.SetColor (currentElementsColor);

		// Increment the player's speed
		playerController.IncrementSpeed();

        // Increment the score
        ScoreManager.instance.IncrementScore();
        scoreText.text = "" + ScoreManager.instance.GetScore();
	}

	public void PlayerDied()
	{
		// Stop wall spawning
		wallSpawner.StopSpawn();

        // Update the high score
        ScoreManager.instance.UpdateHighScore();

		// Load GameOver scene
		StartCoroutine(LoadSceneAfterDelay(2, 1.5f));
	}

	public void PauseGame()
	{
        AudioManager.instance.PauseBackgroundMusic();
		Time.timeScale = 0f;
	}

	public void ResumeGame()
	{
        AudioManager.instance.ResumeBackgroundMusic();
		Time.timeScale = 1f;
	}

    IEnumerator LoadSceneAfterDelay(int scene, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(scene);
    }

}
