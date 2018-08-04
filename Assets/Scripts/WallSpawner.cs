using UnityEngine;
using System.Collections;

public class WallSpawner : MonoBehaviour {

	public GameObject wallPrefab;		//the wall game object
	public int wallPoolSize = 5;		//how many walls to keep on standby
    public float minSpawnRate = 1f;     //minimum value for how quickly walls spawn
    public float maxSpawnRate = 1.5f;   //maximum value for how quickly walls spawn
    //public float distanceBetweenWalls = 2.5f;
    public float wallSeparationFromScreenLimit = 1.25f;

	private float wallMinX;		        //minimum x value of the wall position
	private float wallMaxX;		        //maximum x value of the wall position

	private GameObject[] walls;			//collection of pooled walls
	private int currentWall = 0;		//index of the current wall in the collection

	private Color currentColor = Color.white;

	void Start()
	{
		//initialize the walls collection
		walls = new GameObject[wallPoolSize];

        //initialize the walls opening min and max x position depending on the screen
        Camera mainCamera = Camera.main;
        float screenMinX = mainCamera.ScreenToWorldPoint(new Vector3(0f, 0f, 0f)).x;
        float screenMaxX = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, 0f, 0f)).x;
        wallMinX = screenMinX + wallSeparationFromScreenLimit;
        wallMaxX = screenMaxX - wallSeparationFromScreenLimit;

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
			if (walls[currentWall] == null) {
				walls[currentWall] = (GameObject)Instantiate(wallPrefab);
				foreach (Transform wallPart in walls[currentWall].transform) {
					wallPart.GetComponent<SpriteRenderer> ().color = currentColor;
				}
			}

			//To spawn a wall, get the current spawner position and set a random x position
			Vector3 pos = transform.position;
			pos.x = Random.Range(wallMinX, wallMaxX);
			walls[currentWall].transform.position = pos;

			//increase the value of currentWall. If the new size is too big, set it back to zero
			if (++currentWall >= wallPoolSize) currentWall = 0;
			//leave this coroutine until the proper amount of time has passed
            yield return new WaitForSeconds(Random.Range(minSpawnRate, maxSpawnRate));
		}
	}
}
