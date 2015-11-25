using UnityEngine;
using System;

public class TargetManager : MonoBehaviour {
	TargetScript[] allTargets;
	string TARGET_TAG = "Target";
	void Start()  {
		GameObject[] allTargetsGameObjects = GameObject.FindGameObjectsWithTag (TARGET_TAG);
		allTargets = new TargetScript[allTargetsGameObjects.Length];
		for (int i = 0; i < allTargetsGameObjects.Length; i++) {
			allTargets[i] = allTargetsGameObjects[i].GetComponent<TargetScript>();
		}
	}

	public void GenerateDepencies(TargetScript target) {
		foreach (TargetScript t in target.directDependants) {
			t.AddDependee(target);
		}
	}
}