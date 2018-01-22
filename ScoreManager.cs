using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	public Text scoreText;
	public Text normHighScoreText;
	public Text hardHighScoreText;
	public Text spaceRemainingText;
	public GameObject controller;

	public int score = 0;

	private int normHighScore = 0;
	private int hardHighScore = 0;
	private float spaceRemaining = 100;
	private Controls controls;

	public static bool gameOver = false;


	void Start(){
		controls = controller.GetComponent<Controls> (); 
	}

	void FixedUpdate () {
		scoreText.text = "Score: " + score;
		normHighScoreText.text = "High Score: " + normHighScore + " (normal)";
		hardHighScoreText.text = "High Score: " + hardHighScore + " (hard)";
		spaceRemainingText.text = "Space Remaining: " + spaceRemaining + "%";
		if (spaceRemaining < 1) {
			gameOver = true; 
		}
	}

	public bool checkIfGameOver(){
		if (gameOver) {
			return true;
		} else {
			return false;
		}
	}

	public void newGame(){
		gameOver = false;
	}


	public void updateScore(int points){
		score += points;
		if (controls.hardMode) {
			updateHardHS ();
		} else {
			updateNormalHS ();
		}
	}

	void updateNormalHS(){
		if (score > normHighScore) {
			normHighScore = score;
		}
	}

	void updateHardHS(){
		if (score > hardHighScore) {
			hardHighScore = score;
		}
	}

	public void resetScore(){
		score = 0;
		spaceRemaining = 100;
	}

	public void SpaceRemaining(float _spaceRemaining){
		spaceRemaining = Mathf.Round(_spaceRemaining);
	}

}
