using UnityEngine;
using System.Collections;

public class GoalScript : MonoBehaviour {

	private Collider sphereCollider;
	private GameControllerScript gameController;
	
	void Start () {
		sphereCollider = GetComponent<Collider> ();
		gameController = GameObject.FindObjectOfType<GameControllerScript> ();
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player") {
			other.gameObject.GetComponent<Animator>().SetTrigger("celebrate");
			gameController.IsGameOver = true;
		}
	}
}
