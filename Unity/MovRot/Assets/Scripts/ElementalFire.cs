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
}


