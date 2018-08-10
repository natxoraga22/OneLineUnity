using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;


public class PlayerController : MonoBehaviour {

	// Player components
	private Rigidbody2D rigidBody;
	private SpriteRenderer spriteRenderer;
	private TrailRenderer trailRenderer;

	// Color
	private Color currentColor = Color.white;
	public Sprite whiteSprite;
	public Sprite blackSprite;
	public Sprite redWhiteSprite;
	public Sprite redBlackSprite;

	// Speed
	public float initialSpeed = 3.5f;
	public float speedIncrement = 0.05f;
	public float maxSpeed = 6.5f;
	private float currentSpeed;

	private bool isDead = false;
	private bool stopped = false;

	void Start() 
	{
		rigidBody = GetComponent<Rigidbody2D> ();
		spriteRenderer = GetComponent<SpriteRenderer> ();
		trailRenderer = GetComponent<TrailRenderer> ();

		currentSpeed = initialSpeed;
		rigidBody.velocity = new Vector2 (0.0f, currentSpeed);
	}

	void FixedUpdate() 
	{
        // Used only to test on unity with arrow keys
        /*
        if (!isDead && !stopped) {
            float horizontalMovement = Input.GetAxisRaw("Horizontal");
            rigidBody.velocity = new Vector2(horizontalMovement * currentSpeed, currentSpeed);
        }
        */
	}

    public void MoveLeft() 
    {
        if (!isDead && !stopped) rigidBody.velocity = new Vector2(-currentSpeed, currentSpeed);
    }

    public void MoveRight() 
    {
        if (!isDead && !stopped) rigidBody.velocity = new Vector2(currentSpeed, currentSpeed);
    } 

    public void MoveUp() 
    {
        if (!isDead && !stopped) rigidBody.velocity = new Vector2(0f, currentSpeed);
    }

    public float GetCurrentSpeed() 
    {
        return currentSpeed > 0.001f ? currentSpeed : initialSpeed;
    }

	public void SetColor(Color color)
	{
		currentColor = color;
		if (currentColor == Color.white) spriteRenderer.sprite = whiteSprite;
		else if (currentColor == Color.black) spriteRenderer.sprite = blackSprite;
		trailRenderer.material.color = currentColor;
	}

	public void IncrementSpeed()
	{
		currentSpeed += speedIncrement;
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Wall") {
            isDead = true;

            // Stop the player
            rigidBody.velocity = new Vector2(0f, 0f);

            // Paint player and collider wall red
            if (currentColor == Color.white) spriteRenderer.sprite = redBlackSprite;
            else if (currentColor == Color.black) spriteRenderer.sprite = redWhiteSprite;
            trailRenderer.material.color = Color.red;
            SpriteRenderer wallSpriteRenderer = other.gameObject.GetComponent<SpriteRenderer>();
            wallSpriteRenderer.color = Color.red;

            // Tell the game controller game is over
            GameController.current.PlayerDied();
        }
	}
}
