using System;

public class ElementalIce : Elemental
{
	protected new void Awake() {
		base.Awake ();
		element = Element.ICE;
	}
	
	protected override bool IsConsumableBy(Element otherElement) {
		if (otherElement == Element.NONE || otherElement == element) {
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
