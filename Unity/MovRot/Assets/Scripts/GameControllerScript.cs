using UnityEngine;
using System.Collections;

public class GameControllerScript : MonoBehaviour {

	public CanvasScript canvasScript;
	public PauseMenuScript inGameMenu;

	public int levelId;

	private bool isGameOver = false;
	CharacterScript characterScript;
	bool pause = false;

	public bool IsGameOver {
		get { return isGameOver; }
		set { 
			isGameOver = value; 
			if (isGameOver) {
				StartCoroutine(DisplayGameOverAfter(0.5f));
			}
		}
	}

	// Use this for initialization
	void Start () {
		canvasScript.gameObject.SetActive (false);
		inGameMenu.gameObject.SetActive (false);
		characterScript = GameObject.FindGameObjectWithTag ("Player").GetComponent<CharacterScript> ();
		Time.timeScale = 1f;
	}
	
	// Update is called once per frame
	void Update () {
		bool escPressed = Input.GetButtonUp ("Cancel");
		if (escPressed) {
			pause = !pause;
			inGameMenu.gameObject.SetActive(pause);
			if (pause) {
				Time.timeScale = 0f;
			} else {
				Time.timeScale = 1f;
			}
		}
		if (!pause) {
			float moveVertical = Input.GetAxis ("Vertical");
			float moveHorizontal = Input.GetAxis ("Horizontal");
			characterScript.Move(moveVertical, moveHorizontal);
			bool rotateLeft = Input.GetMouseButton(0);
			bool rotateRight = Input.GetMouseButton(1);
			if (rotateRight) {
				characterScript.RotateClockwise(true);
			} else if (rotateLeft) {
				characterScript.RotateClockwise(false);
			}
		}
	}

	IEnumerator DisplayGameOverAfter(float seconds) {
		canvasScript.gameObject.SetActive (true);
		Debug.Log ("Call to DisplayGameOverAfter made, now yielding");
		yield return new WaitForSeconds(seconds);
		Debug.Log ("Finished yielding, should now display the game over scene");
		canvasScript.Fade (2.0f);
	}

}
