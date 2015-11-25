using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TargetScript : MonoBehaviour {

	private List<TargetScript> directDependees = new List<TargetScript>();	//targets that are dependent on this target being visited
	public void AddDependee(TargetScript target) {
		directDependees.Add (target);
	}
	public TargetScript[] directDependants;	//Targets that this target is dependent on

	private CharacterScript characterScript;
	private TargetManager targetManager;

	private bool completed = false;
	public bool Completed {
		get { return completed; }
	}
	private bool reqMet = false;
	public bool ReqMet {
		get { return reqMet; }
	}
	 
	// Use this for initialization
	protected void Start () {
		characterScript = GameObject.FindGameObjectWithTag ("Player").GetComponent<CharacterScript>();
		targetManager = GameObject.FindGameObjectWithTag ("TargetManager").GetComponent<TargetManager>();
		targetManager.GenerateDepencies (this);
		if (directDependants.Length == 0) {
			NotifyCompleted(this);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnTriggerEnter(Collider other) {
		if (other.gameObject == characterScript.gameObject) {
			if (ReqMet && !completed) {
				completed = true;
				foreach (TargetScript dependee in directDependees) {
					dependee.NotifyCompleted(this);
				}
			}
		}
	}

	bool RequirementsMet() {
		foreach (TargetScript ts in directDependants) {
			if (!ts.ReqMet) {
				return false;
			}
		}
		return true;
	}

	void NotifyCompleted(TargetScript target) {
		if (reqMet = RequirementsMet ()) {
			Debug.Log ("Thank you dependant " + target + ". Now my (" + this + ") requirements are also met, therefore I can be visited for completion");
			Debug.Log ("Do something to signal it...");
		} else {
			Debug.Log ("Thank you dependant " + target + ". I'm (" + this + ") still waiting for the other targets before completion though.");
		}
	}
}
