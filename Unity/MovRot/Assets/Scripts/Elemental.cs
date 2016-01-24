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

	public bool IsSurrounded { get; set; }

	IElementAffectable[] affectables;

	protected void Awake() {
		elementalManager = GameObject.FindGameObjectWithTag ("ElementalManager").GetComponent<ElementalManager>();
		tile = GetComponentInParent<Tile> ();

	}

	protected void Start() {
		timer.listener = this;
		affectables = transform.parent.gameObject.GetComponentsInChildren<IElementAffectable> ();
		foreach (IElementAffectable affectable in affectables) {
			affectable.doAffect(element);
		}
		ConsumedByAdjacent ();
	}

	void Update() {
		if (IsSurrounded) {
			if (!(IsSurrounded = IsSurroundedByEqualElements())) {
				timer.Abort();
				Debug.Log ("Timer aborted for tile " + tile);
			}
		}
	}

	public virtual void ConsumedByAdjacent() {
		if (IsSurroundedByEqualElements () && IsConsumableBy(tile.GridManager.GetTile (tile.GridLoc.WithY (1)).elemental.element)) {
			Element elementToBe = ElementAfterConsumed(GetSurroundingElementKind());
			this.elementToBe = elementToBe;
			timer.StartTimer();
			Debug.Log("tile; " + tile + ", will be changed to " + elementToBe);
		}
	}

	public virtual bool IsSurroundedByEqualElements() {
		Tile[] adjacentTiles = tile.GridManager.GetAdjacentTiles (tile.GridLoc);
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
		return tile.GridManager.GetTile (tile.GridLoc.WithY (1)).elemental.element;
	}

	protected abstract bool IsConsumableBy(Element element);

	public abstract Element ElementAfterConsumed(Element consumer);

	#region Listener implementation

	public virtual void Notify (object o)
	{
		elementalManager.ReplaceElemental (tile, elementToBe);
		foreach (IElementAffectable affectable in affectables) {
			affectable.doAffect(element);
		}
	}

	#endregion
}
