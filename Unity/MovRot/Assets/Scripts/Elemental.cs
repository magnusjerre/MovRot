using UnityEngine;
using System.Collections;

[System.Serializable]
public class Elemental {
	
	public Element element;

	Element[] consumedBy, consumes;
	public Tile tile;

	public bool Consumes(GridManager gridManager) {
		if (element == Element.NONE) {
			return false;
		}

		Tile[] tilesEncircled = gridManager.Encircles (tile, element);
		foreach (Tile tile2 in tilesEncircled) {
			tile2.Surround(element);
			Debug.Log("tile: " + tile2 + ", changed");
		}
		if (tilesEncircled.Length > 0) {
			return true;
		}
		return false;
	}

}
