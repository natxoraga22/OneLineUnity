using UnityEngine;
using System.Collections;

public class WallSpawner : MonoBehaviour {

	public GameObject wallPrefab;		        //the wall game object
    public PlayerController playerController;   //reference to the player

    public float wallSeparationFromScreenLimit = 1.25f;     //min x distance between screen limit and wall opening
    public float distanceBetweenWallsOffset = 1f;           //difference between min and max distance between two consecutive walls

    // Wall position and distance between them
	private float wallMinX;		            //minimum x value of the wall position
	private float wallMaxX;		            //maximum x value of the wall position
    private float minDistanceBetweenWalls;  //min y-distance between two consecutive walls
    private float maxDistanceBetweenWalls;  //max y-distance between two consecutive walls

    // Wall pool
	private GameObject[] walls;     //collection of pooled walls
    int wallPoolSize;               //number of walls to keep pooled
	private int currentWall = 0;	//index of the current wall in the collection

	private Color currentColor = Color.white;

	void Start()
	{
        Camera mainCamera = Camera.main;

        //initialize the walls opening min and max x position depending on the screen
        float screenMinX = mainCamera.ScreenToWorldPoint(new Vector3(0f, 0f, 0f)).x;
        float screenMaxX = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, 0f, 0f)).x;
        wallMinX = screenMinX + wallSeparationFromScreenLimit;
        wallMaxX = screenMaxX - wallSeparationFromScreenLimit;

        //initialize the min and max distance between walls
        minDistanceBetweenWalls = wallMaxX - wallMinX + (playerController.gameObject.GetComponent<SpriteRenderer>().size.y * 2);
        maxDistanceBetweenWalls = minDistanceBetweenWalls + distanceBetweenWallsOffset;

        //initialize the walls collection
        float wallSpaceHeight = gameObject.transform.position.y - (-mainCamera.orthographicSize);
        wallPoolSize = Mathf.CeilToInt(wallSpaceHeight / minDistanceBetweenWalls);
        walls = new GameObject[wallPoolSize];

		//starts our function in charge of spawning the walls in the playable area
		StartCoroutine ("SpawnLoop");
	}

	public void SetColor(Color color)
	{
		currentColor = color;
		foreach (GameObject wall in walls) {
			if (wall != null) {
				foreach (Transform wallPart in wall.transform) {
					wallPart.GetComponent<SpriteRenderer> ().color = currentColor;
				}
			}
		}
	}

	public void StopSpawn()
	{
		//stops our spawning function
		StopCoroutine("SpawnLoop");
	}

    bool even = false;
	IEnumerator SpawnLoop()
	{
		//infinite loop: use with caution
		while (true) 
		{	
			if (walls[currentWall] == null) {
				walls[currentWall] = (GameObject)Instantiate(wallPrefab);
				foreach (Transform wallPart in walls[currentWall].transform) {
					wallPart.GetComponent<SpriteRenderer>().color = currentColor;
				}
			}

			//To spawn a wall, get the current spawner y-position and set a random x-position between the limits
			Vector3 pos = transform.position;
			pos.x = Random.Range(wallMinX, wallMaxX);
			walls[currentWall].transform.position = pos;

			//increase the value of currentWall. If the new size is too big, set it back to zero
			if (++currentWall >= wallPoolSize) currentWall = 0;

            //leave this coroutine until the proper amount of time has passed
            float distanceBetweenWalls = Random.Range(minDistanceBetweenWalls, maxDistanceBetweenWalls);
            yield return new WaitForSeconds(distanceBetweenWalls / playerController.GetCurrentSpeed());
		}
	}
}
