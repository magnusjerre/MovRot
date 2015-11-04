using UnityEngine;
using System.Collections;

public abstract class Elemental : MonoBehaviour {
	
	protected Element element;
	public Element Element { get { return element; } }

	protected Element[] consumedBy, consumes;
	protected Tile tile;

	void Start() {
		tile = GetComponentInParent<Tile> ();
	}

	public virtual void ConsumedByAdjacent(GridManager gridManager) {
		if (IsSurroundedByEqualElements (gridManager)) {
			Element elementToBe = GetSurroundingElementKind(gridManager);
			tile.Surround(elementToBe);
			Debug.Log("woho, " + tile + ", will be changed to " + ElementAfterConsumed(element));
		}
	}

	public virtual bool ConsumesAnyAdjacentElementals(GridManager gridManager) {
		Tile[] adjacentTiles = new Tile[4];
		Tile[] consumedTiles = new Tile[4];
		int i = 0;
		bool consumedAnything = false;
		adjacentTiles = gridManager.GetAdjacentTiles (tile.GridLoc);
		foreach (Tile t in adjacentTiles) {
			if (t != null) {
				if (Consumes(this, t.elemental)) {
					consumedTiles[i] = t;
					t.Surround(t.elemental.ElementAfterConsumed(element));
					Debug.Log("tile: " + t + ", will be changed to: " + t.elemental.ElementAfterConsumed(element));
					consumedAnything = true;
				}
			} else {
				consumedTiles[i] = null;
			}
			i++;
		}
		return consumedAnything;
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

	public abstract Element ElementAfterConsumed(Element consumer);

}
