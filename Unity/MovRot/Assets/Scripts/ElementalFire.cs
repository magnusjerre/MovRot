using System;
public class ElementalFire : Elemental
{
	protected new void Awake() {
		base.Awake ();
		element = Element.FIRE;
	}

	protected override bool IsConsumableBy(Element otherElement) {
		if (otherElement == Element.NONE || otherElement == element) {
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


