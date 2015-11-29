using UnityEngine;
using System.Collections;
using System;

public class Tile : MonoBehaviour, Listener, ITraversable {

	public Loc2D GridLoc;
	public Timer timer;
	
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

	private ElementalManager elementalManager;

	// Use this for initialization
	void Start () {
		gridManager = GetComponentInParent<GridManager> ();
		elemental = GetComponentInChildren<Elemental> ();
		elementalManager = GameObject.FindGameObjectWithTag ("ElementalManager").GetComponent<ElementalManager>();
		timer = new Timer (this, 1f);
	}
	
	// Update is called once per frame
	void Update () {
		if (IsSurrounded) {
			if (IsSurrounded = IsEncircled()) {
				timer.Update (Time.deltaTime);
			} else {
				timer.Abort();
				Debug.Log ("Timer aborted for tile " + this);
			}
		}
	}

	public void Surround(Element e) {
		IsSurrounded = true;
		elementToBe = e;
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
		Debug.Log ("Notified " + GridLoc + " of timer finished. Going to reset timer and change elemental to " + elementToBe);  
		((Timer)timer).Reset ();
		if (elemental.IsSurroundedByEqualElements (gridManager)) {
			Debug.Log ("Tile still surrounded by other tiles of same element. Starting the timer a new");
			((Timer)timer).Start ();
			elementToBe = elemental.ElementAfterConsumed(elemental.GetSurroundingElementKind(gridManager));
		}
		GameObject newElemental = elementalManager.GetElemental (elementToBe);
		newElemental.transform.SetParent (transform);
		newElemental.transform.localPosition = Vector3.zero;
		elemental.transform.parent = null;
		Destroy (elemental.gameObject);
		elemental = newElemental.GetComponent<Elemental> ();
	}
	#endregion

	#region ITraversable implementation

	public bool IsTraversable ()
	{
		ITraversable[] traversables = GetComponentsInChildren<ITraversable> ();
		foreach (ITraversable traversable in traversables) {
			if (traversable != this && !traversable.IsTraversable()) {
				return false;
			}
		}
		return true;
	}

	#endregion
}
