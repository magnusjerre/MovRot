using System;
using UnityEngine;

public class ElementalNone : Elemental
{
	void Start() {
		tile = GetComponentInParent<Tile> ();
		element = Element.NONE;
	}
	
	protected override bool IsConsumabledBy(Elemental elemental) {
		if (elemental.Element == Element.NONE) {
			return false;
		}
		return true;
	}

	public override Element ElementAfterConsumed(Element consumer) {
		return consumer;
	}

	#region implemented abstract members of Elemental

	public override bool IsTraversable ()
	{
		return true;
	}

	#endregion
}