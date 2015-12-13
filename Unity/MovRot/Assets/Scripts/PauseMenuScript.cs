using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PauseMenuScript : MonoBehaviour
{
	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void RestartLevel() {
		Application.LoadLevel (Application.loadedLevel);
	}

	public void LoadMenu() {
		Application.LoadLevel (GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameControllerScript> ().levelId);
	}
}

