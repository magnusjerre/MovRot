using System;
using UnityEngine;

public class ElementalNone : Elemental
{
	void Start() {
		tile = GetComponentInParent<Tile> ();
		element = Element.NONE;
	}

	public override bool ConsumesAnyAdjacentElementals(GridManager gridManager) {
		return false;
	}
	
	protected override bool IsConsumabledBy(Elemental elemental) {
		if (elemental.Element == Element.NONE) {
			return false;
		}
		return true;
	}
}