using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controls : MonoBehaviour {

	public GameObject scoreManagerGO;
	private ScoreManager scoreManager;

	public int scorePenalty = 7;
	public float force = 10;
	public bool hardMode = true;
	public GameObject[] ballGrids;
	public bool controllerToggle = false;

	private Vector2 gravityDirection = new Vector2 (0, 0);
	private int numOfGrids;

	private float remainingSpace = 0;


	void Start () {
		scoreManager = scoreManagerGO.GetComponent<ScoreManager> ();
		Physics2D.gravity = gravityDirection;
		setDifficulty ();
	}

	void Update () {
		if (controllerToggle) {
			upDownLeftRight ();
			mouseClick ();
		}
	}

	public void resetGravity(){
		gravityDirection = new Vector2(0f, 0f);
		Physics2D.gravity = gravityDirection;
	}

	public void setDifficulty(){
		if (hardMode) {
			numOfGrids = ballGrids.Length;
		} else {
			numOfGrids = 2;
		}
	}

	void mouseClick(){
		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit2D hit = Physics2D.GetRayIntersection (ray, Mathf.Infinity);
			if (hit.collider != null) {
				GameObject hitBall = hit.transform.gameObject;
				Ball newBall = hitBall.GetComponent<Ball> ();
				if (newBall.gameObject.tag == "Ball" && newBall.ballSize < 200) {
					int points = newBall.ballSize * scorePenalty; 
					scoreManager.updateScore (-points);
					Destroy (hitBall);
				}
			}
		}
	}


	void upDownLeftRight(){
		if (Input.GetKeyDown (KeyCode.DownArrow)) {	
			onButtonPress (0f, -force);
		}
		if (Input.GetKeyDown (KeyCode.UpArrow)) {	
			onButtonPress(0f, force);
		}
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			onButtonPress(-force, 0f);
		}
		if (Input.GetKeyDown (KeyCode.RightArrow)) 	{
			onButtonPress(force, 0f);
		}
	}

	void onButtonPress(float vectorX, float vectorY){
		checkSpaces ();
		spawnNext ();
		gravityDirection = new Vector2(vectorX, vectorY);
		Physics2D.gravity = gravityDirection;
	}

	void checkSpaces(){
		remainingSpace = 0;
		foreach (GameObject grid in ballGrids) {
			BallGrid colorGrid = grid.GetComponent<BallGrid> ();
			float gridRemainingSpace = colorGrid.spaceRemainingPercentage ();
			if (gridRemainingSpace > remainingSpace) {
				remainingSpace = gridRemainingSpace;
			}
		}
		scoreManager.SpaceRemaining (remainingSpace);
	}

	void spawnNext(){
		int loopCatcher = 10;
		int randomIndex = Random.Range (0, numOfGrids);
		bool trySpawn = true;
		while (trySpawn) {			
			GameObject grid = ballGrids [randomIndex];
			BallGrid colorGrid = grid.GetComponent<BallGrid> ();
			if (colorGrid.enoughSpaces) {				
				colorGrid.SpawnBall ();
				trySpawn = false;
			
			} else {
				randomIndex = Random.Range (0, numOfGrids);
				if (colorGrid.enoughSpaces) {				
					colorGrid.SpawnBall ();
					trySpawn = false;
					loopCatcher--;
					if (loopCatcher <= 0) {
						trySpawn = false;
					}
				}
			}
		}
	}

	public void resetAllGrids(){
		foreach (GameObject grid in ballGrids) {
			BallGrid colorGrid = grid.GetComponent<BallGrid> ();
			colorGrid.resetEnoughSpaces ();
		}
	}

}
