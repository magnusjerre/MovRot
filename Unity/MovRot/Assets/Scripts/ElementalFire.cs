using System;
using UnityEngine;

public class ElementalFire : Elemental
{
	protected new void Awake() {
		base.Awake ();
		element = Element.FIRE;
	}

	protected new void Start() {
		base.Start ();
		foreach (Burnable burnable in transform.parent.gameObject.GetComponentsInChildren<Burnable>()) {
			if (burnable.IsBurnable) {
				burnable.StartBurn ();
			}
		}
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

	public override void Notify (object o)
	{
		foreach (Burnable burnable in transform.parent.gameObject.GetComponentsInChildren<Burnable>()) {
			if (burnable.IsBurnable) {
				burnable.PauseBurn ();
			}
		}
		base.Notify (o);
	}

}


