using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallGrid : MonoBehaviour {

	private GameObject[] Gridpoints;
	private List<GameObject> emptySpaces = new List<GameObject>();
	public int numberOfSpaces;

	public bool enoughSpaces = true;
	public float ballSize;
	public GameObject ballManager;
	public GameObject ballType;

	void Start(){
		Gridpoints = GameObject.FindGameObjectsWithTag ("Gridpoint");
	}

	public void SpawnBall () {
		FindEmptySpaces ();
		if (enoughSpaces) {
			SpawnAtRandomIndex ();
		}
	}

	public void resetEnoughSpaces(){
		enoughSpaces = true;
	}

	void FindEmptySpaces(){ 
		enoughSpaces = true;
		emptySpaces.Clear ();
		foreach (GameObject point in Gridpoints) {
			Collider2D[] col = Physics2D.OverlapCircleAll (point.transform.position, ballSize);
			if (col.Length == 0) {
				emptySpaces.Add (point);
			}
		}
		numberOfSpaces = emptySpaces.Count;
		if (numberOfSpaces < 1) {
			enoughSpaces = false;
		}
	}

	public float spaceRemainingPercentage(){
		float totalSpaces = Gridpoints.Length;
		float spaceTaken = 0;
		foreach (GameObject point in Gridpoints) {
			Collider2D[] col = Physics2D.OverlapCircleAll (point.transform.position, ballSize);
			if (col.Length == 0) {
				spaceTaken++;
			}
		}
		float remainingSpace = (spaceTaken / totalSpaces) * 100;
		if (remainingSpace >= 1) {
			resetEnoughSpaces ();
		}
		return remainingSpace;
	}


	void SpawnAtRandomIndex(){
		int randomIndex = Random.Range(0,numberOfSpaces); 
		GameObject newSpace = emptySpaces [randomIndex];
		Vector3 newLocation = newSpace.transform.position;
		GameObject newBall = Instantiate (ballType, newLocation, Quaternion.identity) as GameObject;
		newBall.transform.parent = ballManager.transform;
	}


}
