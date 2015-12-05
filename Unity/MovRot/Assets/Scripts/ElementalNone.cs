using System;
using UnityEngine;

public class ElementalNone : Elemental
{
	protected new void Awake() {
		base.Awake ();
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
}