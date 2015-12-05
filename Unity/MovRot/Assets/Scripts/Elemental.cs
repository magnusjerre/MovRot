using UnityEngine;
using System.Collections;
using System;

public abstract class Elemental : MonoBehaviour, Listener {
	
	protected Element element;
	public Element Element { get { return element; } }

	protected Element[] consumedBy, consumes;
	public Tile tile;

	public Timer timer;
	public Element elementToBe;
	private ElementalManager elementalManager;

	protected void Awake() {
		elementalManager = GameObject.FindGameObjectWithTag ("ElementalManager").GetComponent<ElementalManager>();
		tile = GetComponentInParent<Tile> ();
	}

	protected void Start() {
		timer.listener = this;
		ConsumedByAdjacent (tile.GridManager);
	}

	void Update() {
		if (tile.IsSurrounded) {
			if (!(tile.IsSurrounded = IsEncircled())) {
				timer.Abort();
				Debug.Log ("Timer aborted for tile " + tile);
			}
		}
	}

	public virtual void ConsumedByAdjacent(GridManager gridManager) {
		if (IsSurroundedByEqualElements (gridManager)) {
			Element elementToBe = ElementAfterConsumed(GetSurroundingElementKind(gridManager));
			this.elementToBe = elementToBe;
			timer.StartTimer();
			Debug.Log("tile; " + tile + ", will be changed to " + elementToBe);
		}
	}

	public virtual bool IsSurroundedByEqualElements() {
		return IsSurroundedByEqualElements (tile.GridManager);
	}

	public virtual bool IsSurroundedByEqualElements(GridManager gridManager) {
		Tile[] adjacentTiles = gridManager.GetAdjacentTiles (tile.GridLoc);
		Element[] elements = new Element[4];

		for (int i = 0; i < adjacentTiles.Length; i++) {
			if (adjacentTiles [i] == null) {
				return false;
			}
			elements [i] = adjacentTiles [i].elemental.element;
		}

		for (int i = 1; i < adjacentTiles.Length; i++) {
			if (elements[i-1] != elements[i]) {
				return false;
			}
		}

		return true;
	}

	public virtual Element GetSurroundingElementKind() {
		return GetSurroundingElementKind (tile.GridManager);
	}

	public virtual Element GetSurroundingElementKind(GridManager gridManager) {
		return gridManager.GetTile (tile.GridLoc.WithY (1)).elemental.element;
	}

	protected abstract bool IsConsumabledBy(Elemental elemental);

	protected bool Consumes(Elemental consumer, Elemental consumee) {
		if (consumee.IsConsumabledBy (consumer)) {
			Tile[] adjacentTiles = consumee.tile.GridManager.GetAdjacentTiles(consumee.tile.GridLoc);
			foreach (Tile t in adjacentTiles) {
				if (t == null) {
					return false;
				}
				if (t.elemental.element != consumer.element) {
					return false;
				}
			}
			return true;
		}

		return false;
	}

	public bool IsEncircled() {
		bool result = false;
		foreach (Element e in Enum.GetValues(typeof(Element))) {
			if (e != element && e != Element.NONE) {
				if (tile.GridManager.Encircled(tile.GridLoc, e)) {
					result = true;
					break;
				}
			}
		}
		return result;
	}

	public abstract Element ElementAfterConsumed(Element consumer);

	#region Listener implementation

	public void Notify (object o)
	{
		elementalManager.ReplaceElemental (tile, elementToBe);
	}

	#endregion
}
