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
					t.Surround(element);
					Debug.Log("tile: " + t + ", will be changed");
					consumedAnything = true;
				}
			} else {
				consumedTiles[i] = null;
			}
			i++;
		}
		return consumedAnything;
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

}
