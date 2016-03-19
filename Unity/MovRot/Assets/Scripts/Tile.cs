using UnityEngine;
using System.Collections;
using System;

public class Tile : MonoBehaviour, ITraversable {

	private GridManager gridManager;
	public GridManager GridManager { get { return gridManager; } }

	public Loc2D GridLoc;
	public Elemental elemental;
	public bool IsStatic = false;

	public int HP = 5;
	public ActionType[] actionTypes = new ActionType[0];
	public void PerformAction(ActionType type) {
		if (Array.Exists (actionTypes, at => type == at)) {
			GetComponent<MaterialChanger> ().NextMaterial ();
			GetComponentInChildren<ParticleBurstEmitter> ().Emit ();
			if (--HP < 1) {
				GridElement gridElement = GetComponentInChildren<GridElement>();
                if (gridElement != null)
                {
				    GetComponentInChildren<GridElement>().NotifyTileDestroyed(this);
                }
				GetComponentInChildren<Elemental> ().gameObject.SetActive (false);
				GetComponentInChildren<ParticleBurstEmitter> ().RemoveWhenFinished ();
				//Destroy();
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

	void Awake() {
		gridManager = GetComponentInParent<GridManager> ();
		elemental = GetComponentInChildren<Elemental> ();
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	public override string ToString ()
	{
		return "gridX: " + GridLoc.x + ", gridY: " + GridLoc.y;
	}

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
