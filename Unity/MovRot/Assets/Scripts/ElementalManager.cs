using UnityEngine;
using System;

public class ElementalManager : MonoBehaviour {

	public GameObject ElementalNonePrefab;
	public GameObject ElementalIcePrefab;
	public GameObject ElementalFirePrefab;

	void Start() {
	}

	public GameObject GetElemental(Element targetElement) {
		switch (targetElement) {
			case Element.FIRE: return Instantiate(ElementalFirePrefab);
			case Element.ICE: return Instantiate(ElementalIcePrefab);
			default : return Instantiate(ElementalNonePrefab);		
		} 
	}

	public void ReplaceElemental(Tile tile, Element newElement) {
		GameObject oldElemental = tile.elemental.gameObject;
		GameObject newElemental = GetElemental (newElement);	
		Elemental newE = newElemental.GetComponent<Elemental> ();

		newElemental.transform.SetParent (tile.transform);
		newElemental.transform.localPosition = Vector3.zero;
		newE.tile = tile;
		tile.elemental = newE;

		oldElemental.SetActive (false);

	}

}