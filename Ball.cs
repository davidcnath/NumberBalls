using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

	public int ballSize;
	private GameObject ballManager;
	private BallManager manager;

	void Start(){
		ballManager = transform.parent.gameObject;
		manager = ballManager.GetComponent<BallManager> ();
		if (transform.localScale.x < 2f) {
			growBall ();
		}
	}

	void OnCollisionEnter2D(Collision2D coll){
		if (coll.gameObject.tag == "Ball") {
			Ball otherBall = coll.gameObject.GetComponent<Ball> ();
			int otherBallSize = otherBall.ballSize;
			Vector2 middlePosition = (transform.position + coll.transform.position) / 2;
			if (ballSize < 1000 && ballSize == otherBallSize) {				
				if (!manager.collisionFlagged) {
					manager.collisionFlagged = true;
				} else if (manager.collisionFlagged) {
					manager.spawnNewBall (ballSize, middlePosition);
					manager.collisionFlagged = false;
				}
				Destroy (gameObject);
			}
		}
	}

	void growBall(){
		Vector2 startScale = transform.localScale;
		Vector2 endScale = new Vector2 (1.5f, 1.5f);
		transform.localScale = Vector2.Lerp (startScale, endScale, 5f * Time.time);
	}





}
