using UnityEngine;
using System.Collections;
using System;

public class Tile : MonoBehaviour {

	public Loc2D GridLoc;
	
	public bool IsStatic = false;
	
	public Element element;
	public Elemental elemental;

	public int HP = 5;
	public ActionType[] actionTypes = new ActionType[0];
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
		elemental.tile = this;
		elemental.element = element;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override string ToString ()
	{
		return "gridX: " + GridLoc.x + ", gridY: " + GridLoc.y;
	}
}
