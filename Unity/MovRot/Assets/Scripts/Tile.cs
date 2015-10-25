using UnityEngine;
using System.Collections;
using System;

public class Tile : MonoBehaviour {

	public Loc2D GridLoc;
	
	public bool IsStatic = false;

	public int HP = 5;
	public ActionType[] actionTypes;
	public void PerformAction(ActionType type) {
		if (Array.Exists (actionTypes, at => type == at)) {
			if (--HP < 1) {
				Destroy();
			}
		}
	}
	private bool isDestroyed = false;
	public bool IsDestroyed { get { return isDestroyed; } }
	public void Destroy() {
		isDestroyed = true;
		gridManager.RemoveTile (this);
		gameObject.SetActive (false);
	}

	private GridManager gridManager;

	// Use this for initialization
	void Start () {
		gridManager = GetComponentInParent<GridManager> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override string ToString ()
	{
		return "gridX: " + GridLoc.x + ", gridY: " + GridLoc.y;
	}
}
