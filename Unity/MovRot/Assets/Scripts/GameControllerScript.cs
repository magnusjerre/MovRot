using UnityEngine;
using System.Collections;

public class GameControllerScript : MonoBehaviour {

	public CanvasScript canvasScript;

	private bool isGameOver = false;
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
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator DisplayGameOverAfter(float seconds) {
		canvasScript.gameObject.SetActive (true);
		Debug.Log ("Call to DisplayGameOverAfter made, now yielding");
		yield return new WaitForSeconds(seconds);
		Debug.Log ("Finished yielding, should now display the game over scene");
		canvasScript.Fade (2.0f);
	}
}
