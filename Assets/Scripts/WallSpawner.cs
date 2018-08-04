using UnityEngine;
using System.Collections;

public class WallSpawner : MonoBehaviour {

	public GameObject wallPrefab;		        //the wall game object
    public PlayerController playerController;   //reference to the player

    public float wallSeparationFromScreenLimit = 1.25f;     //min x distance between screen limit and wall opening
    public float distanceBetweenWallsOffset = 2f;           //difference between min and max distance between two consecutive walls

    // Wall position
	private float wallMinX;		    //minimum x value of the wall position
	private float wallMaxX;		    //maximum x value of the wall position
    private float nextWallXPosition;
    private bool firstWall = true;

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
        nextWallXPosition = Random.Range(wallMinX, wallMaxX);

        //initialize the walls collection
        wallPoolSize = Mathf.CeilToInt(mainCamera.orthographicSize * 2f);
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

	IEnumerator SpawnLoop()
	{
		//infinite loop: use with caution
		while (true) 
		{	
            //wall instantiation if not exists
			if (walls[currentWall] == null) {
				walls[currentWall] = (GameObject)Instantiate(wallPrefab);
				foreach (Transform wallPart in walls[currentWall].transform) {
					wallPart.GetComponent<SpriteRenderer>().color = currentColor;
				}
			}

            //current wall position
            float currentWallXPosition = nextWallXPosition;
            walls[currentWall].transform.position = new Vector3(currentWallXPosition, transform.position.y, transform.position.z);

            //next wall x-position
            nextWallXPosition = Random.Range(wallMinX, wallMaxX);

			//increase the value of currentWall
			if (++currentWall >= wallPoolSize) currentWall = 0;

            //calculate distance between current wall and next one
            float minDistanceBetweenWalls = Mathf.Abs(nextWallXPosition - currentWallXPosition) + (playerController.gameObject.GetComponent<SpriteRenderer>().size.y * 2);
            float maxDistanceBetweenWalls = minDistanceBetweenWalls + distanceBetweenWallsOffset;

            //leave this coroutine until the proper amount of time has passed
            float distanceBetweenWalls = Random.Range(minDistanceBetweenWalls, maxDistanceBetweenWalls);
            yield return new WaitForSeconds(distanceBetweenWalls / playerController.GetCurrentSpeed());
		}
	}
}
