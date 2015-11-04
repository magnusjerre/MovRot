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

}