using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour {

	public Transform target;
	public float yOffset;

	void Update () {
		transform.position = new Vector3 (transform.position.x, 
										  target.position.y + yOffset, 
										  transform.position.z);
	}
}
