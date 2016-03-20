using UnityEngine;
using System.Collections;

public class GameControllerScript : MonoBehaviour, Listener {

	public CanvasScript canvasScript;
	public PauseMenuScript inGameMenu;
	private GridManager gridManager;
	private TimedScaling timedScaling;

	public int levelId;

	private bool isGameOver = false;
	private bool completedGame = false;
	CharacterScript characterScript;
	bool pause = false;

	public bool IsGameOver {
		get { return isGameOver; }
	}

	// Use this for initialization
	void Start () {
		canvasScript.gameObject.SetActive (false);
		timedScaling = GetComponent<TimedScaling> ();
		inGameMenu.gameObject.SetActive (false);
		characterScript = GameObject.FindGameObjectWithTag ("Player").GetComponent<CharacterScript> ();
		Time.timeScale = 1f;
		gridManager = GameObject.FindGameObjectWithTag ("Grid").GetComponent<GridManager>();
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

	public void DisplayGameOver() {
		StartCoroutine(DisplayGameEndedCanvas("Game over...", true, false, 0.5f));
	}

	public void DisplayGameWon() {
		MoveScript ms = characterScript.GetComponent<MoveScript> ();
		ms.AddListener (this);
		StartCoroutine(DisplayGameEndedCanvas("Victory!", true, true, 0.5f));
	}

	IEnumerator DisplayGameEndedCanvas(string textValue, bool gameOver, bool completed, float seconds) {
		isGameOver = gameOver;
		completedGame = completed;
		canvasScript.gameObject.SetActive(true);
		yield return new WaitForSeconds(seconds);
		canvasScript.Fade (textValue, seconds, false);
	}

	#region Listener implementation
	public void Notify (object o)
	{
		timedScaling.gameObject.SetActive (true);
		foreach (Tile tile in gridManager.GetAllOtherTilesThan(characterScript.GridLoc())) {
			timedScaling.Add (tile.transform);
			ParticleSystem[] particleSystems = tile.GetComponentsInChildren<ParticleSystem> ();
			foreach (ParticleSystem particleSystem in particleSystems) {
				timedScaling.Add (particleSystem.transform);
			}
		}
		timedScaling.RunScaling ();
	}
	#endregion
}
