using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour {

	public GameObject scoreManagerGO;
	public GameObject menu;
	public GameObject controller;
	public Button mediumButton;
	public Button hardButton;
	public Button endGame;
	public GameObject ballManagerGO;

	public GameObject endGameMenu;
	public Text finalScore;
	public Button mainMenu;
	public bool gameIsOver = false;

	private ScoreManager scoreManager;
	private bool gameMenuActive = true; 
	private Controls controls;
	private BallManager ballManager;


	void Start () {
		endGameMenu.SetActive (false);
		endGame.gameObject.SetActive (false);

		scoreManager = scoreManagerGO.GetComponent<ScoreManager> ();
		ballManager = ballManagerGO.GetComponent<BallManager> ();
		controls = controller.GetComponent<Controls> (); 
		Button startMedBtn = mediumButton.GetComponent<Button> ();
		Button startHardBtn = hardButton.GetComponent<Button> ();
		Button endBtn = endGame.GetComponent<Button> ();
		Button mainMenuBtn = mainMenu.GetComponent<Button> ();

		startMedBtn.onClick.AddListener (newMedGameButton);
		startHardBtn.onClick.AddListener (newHardGameButton);
		endBtn.onClick.AddListener (endGameButton);
		mainMenuBtn.onClick.AddListener (mainMenuButton);
	}

	void Update(){
		if (!gameMenuActive) {
			gameIsOver = scoreManager.checkIfGameOver ();
		}
		if (gameIsOver) {
			ballManager.clearAllBalls ();
			controls.controllerToggle = false;
			endGame.gameObject.SetActive (false);
			endGameMenu.SetActive (true);
		} else {			
			endGameMenu.SetActive (false);
		}
	}

	void FixedUpdate(){
		finalScore.text = "Final Score: " + scoreManager.score;
	}

	void newMedGameButton(){
		controls.hardMode = false;
		controls.setDifficulty ();
		controls.resetAllGrids ();
		controls.controllerToggle = true;
		toggleGameMenu ();
		ballManager.spawnFirstBall ();
		scoreManager.newGame ();
	}
	void newHardGameButton(){
		controls.hardMode = true;
		controls.setDifficulty ();
		controls.resetAllGrids ();
		controls.controllerToggle = true;
		toggleGameMenu ();
		ballManager.spawnFirstBall ();
		scoreManager.newGame ();
	}
	void endGameButton(){
		ballManager.clearAllBalls ();
		scoreManager.resetScore ();
		controls.controllerToggle = false;
		controls.resetGravity ();
		toggleGameMenu ();
	}
	void mainMenuButton(){
		ballManager.clearAllBalls ();
		scoreManager.resetScore ();
		scoreManager.newGame ();
		controls.controllerToggle = false;
		controls.resetGravity ();
		gameIsOver = false;
		endGameMenu.SetActive (false);
		toggleGameMenu ();
	}

	public void toggleGameMenu(){
		if (!gameMenuActive) {
			endGame.gameObject.SetActive (false);
			menu.SetActive (true);
			gameMenuActive = true;
		} else {
			endGame.gameObject.SetActive (true);
			menu.SetActive(false);
			gameMenuActive = false;
		}
	}

}
