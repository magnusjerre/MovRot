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
        Debug.Log("inside goal");
		if (other.gameObject.tag == "Player") {
			if (Completed) {
                ShowCompleted();
				other.gameObject.GetComponent<Animator>().SetTrigger("celebrate");
				gameController.IsGameOver = true;
			}
		}
	}

    public override void ShowRequirementsNotMet()
    {
        Debug.Log("Target goal script");
        //Disse skal vises
        foreach (Renderer renderer in diamondRenderers)
        {
            //renderer.gameObject.SetActive(false);
        }
    }

}