using System;

public class ElementalIce : Elemental
{
	void Start() {
		tile = GetComponentInParent<Tile> ();
		element = Element.ICE;
	}
	
	protected override bool IsConsumabledBy(Elemental elemental) {
		if (elemental.Element == Element.NONE || elemental.Element == element) {
			return false;
		}
		return true;
	}

	public override Element ElementAfterConsumed(Element consumer) {
		if (consumer == Element.FIRE) {
			return Element.NONE;
		}
		return element;
	}
}
