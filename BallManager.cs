using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour {

	public GameObject scoreManagerGO;
	private ScoreManager scoreManager;

	public bool collisionFlagged = false;
	public GameObject[] allBalls;

	private int ballSize;
	private GameObject ballType;
	private bool tooManyBalls = false;



	void Start(){
		scoreManager = scoreManagerGO.GetComponent<ScoreManager> ();
	}

	public void checkBallCount(){
		int childrenBalls = transform.childCount;
		if (childrenBalls > 400) {
			tooManyBalls = true;
			Debug.Log ("Bug: Too many balls");
		} else { tooManyBalls = false;
		}
	}

	public void spawnFirstBall(){
		Vector2 start = new Vector2 (0f, 0f);
		GameObject newBall = Instantiate (allBalls[0], start, Quaternion.identity) as GameObject;
		newBall.transform.parent = gameObject.transform;
	}

	public void spawnNewBall(int ballSize, Vector2 collPosition){
		findNextBallType (ballSize);
		checkBallCount ();
		scoreManager.updateScore (ballSize * 2);
		if (!tooManyBalls) {
			ballType.transform.localScale = new Vector2 (0.5f, 0.5f);
			GameObject newBall = Instantiate (ballType, collPosition, Quaternion.identity) as GameObject;
			newBall.transform.parent = gameObject.transform;
			collisionFlagged = false;
		}
	}

	void findNextBallType(int ballSize){
		int nextBallSize = ballSize * 2;
		if (nextBallSize == 2) 			{	ballType = allBalls [0];	} 
		else if (nextBallSize == 4) 	{	ballType = allBalls [1];	} 
		else if (nextBallSize == 8) 	{	ballType = allBalls [2];	} 
		else if (nextBallSize == 16) 	{	ballType = allBalls [3];	}
		else if (nextBallSize == 32) 	{	ballType = allBalls [4];	}
		else if (nextBallSize == 64) 	{	ballType = allBalls [5];	}
		else if (nextBallSize == 3) 	{	ballType = allBalls [6];	} 
		else if (nextBallSize == 6) 	{	ballType = allBalls [7];	}
		else if (nextBallSize == 12) 	{	ballType = allBalls [8];	}
		else if (nextBallSize == 24) 	{	ballType = allBalls [9];	}
		else if (nextBallSize == 48) 	{	ballType = allBalls [10];	}
		else if (nextBallSize == 96) 	{	ballType = allBalls [11];	}
		else if (nextBallSize == 5) 	{	ballType = allBalls [12];	} 
		else if (nextBallSize == 10) 	{	ballType = allBalls [13];	}
		else if (nextBallSize == 20) 	{	ballType = allBalls [14];	}
		else if (nextBallSize == 40) 	{	ballType = allBalls [15];	}
		else if (nextBallSize == 80)	{	ballType = allBalls [16];	}
		else if (nextBallSize == 160) 	{	ballType = allBalls [17];	}
		else if (nextBallSize == 128 || nextBallSize == 192 || nextBallSize == 320) 	{	ballType = allBalls [18];	}
		else if (nextBallSize == 400) 	{	ballType = allBalls [19];	}
		else if (nextBallSize == 800) 	{	ballType = allBalls [20];	}
		else if (nextBallSize == 1600) 	{	ballType = allBalls [21];	}
		else {	Debug.Log ("BallSize not found - See BallManager Script");}
	}

	public void clearAllBalls(){
		foreach (Transform child in transform) {
			Destroy (child.gameObject);
		}
	}

}
