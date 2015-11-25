using UnityEngine;
using System;

public class TargetGoalScript : TargetScript {
	
	private GameControllerScript gameController;
	
	protected new void Start () {
		base.Start ();
		gameController = GameObject.FindObjectOfType<GameControllerScript> ();
	}
	
	protected new void OnTriggerEnter(Collider other) {
		base.OnTriggerEnter(other);
		if (other.gameObject.tag == "Player") {
			if (Completed) {
				other.gameObject.GetComponent<Animator>().SetTrigger("celebrate");
				gameController.IsGameOver = true;
			}
		}
	}

}