using UnityEngine;
using System.Collections;
using System;

public class Tile : MonoBehaviour, Listener {

	public Loc2D GridLoc;
	private Timer timer;
	
	public bool IsStatic = false;
	
	public Element elementToBe;
	public Elemental elemental;
	public bool IsSurrounded { get; set; }

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
	public GridManager GridManager { get { return gridManager; } }

	// Use this for initialization
	void Start () {
		gridManager = GetComponentInParent<GridManager> ();
		elemental = GetComponentInChildren<Elemental> ();
		timer = new Timer (this, 1f);
	}
	
	// Update is called once per frame
	void Update () {
		if (IsSurrounded) {
			if (IsSurrounded = IsEncircled()) {
				timer.Update (Time.deltaTime);
			} else {
				timer.Reset();
			}
		}
	}

	public void Surround(Element e) {
		IsSurrounded = true;
		timer.Start ();
	}

	private bool IsEncircled() {
		bool result = false;
		foreach (Element e in Enum.GetValues(typeof(Element))) {
			if (e != elemental.Element && e != Element.NONE) {
				if (gridManager.Encircled(GridLoc, e)) {
					result = true;
					break;
				}
			}
		}
		return result;
	}



	public override string ToString ()
	{
		return "gridX: " + GridLoc.x + ", gridY: " + GridLoc.y;
	}


	#region Listener implementation
	public void Notify (object timer)
	{
		Debug.Log ("Notified: " + GridLoc);  
		((Timer)timer).Reset ();
	}
	#endregion
}
