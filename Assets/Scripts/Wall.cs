using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour {
    
	void OnTriggerEnter2D(Collider2D other)
	{
		//if the player hits the trigger collider in between the walls then increment the score
		GameController.current.PlayerScored();
	}

}
