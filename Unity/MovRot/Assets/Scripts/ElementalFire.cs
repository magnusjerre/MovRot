using System;
public class ElementalFire : Elemental
{
	void Start() {
		tile = GetComponentInParent<Tile> ();
		element = Element.FIRE;
	}

	protected override bool IsConsumabledBy(Elemental elemental) {
		if (elemental.Element == Element.NONE || elemental.Element == element) {
			return false;
		}
		return true;
	}

	public override Element ElementAfterConsumed(Element consumer) {
		if (consumer == Element.ICE) {
			return Element.NONE;
		}
		return element;
	}
}


