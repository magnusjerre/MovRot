using UnityEngine;
using System.Collections;

public class ParticleBurstEmitter : MonoBehaviour
{

	private ParticleSystem pSystem;
	private int emitRate;
	private bool finished;

	// Use this for initialization
	void Start ()
	{
		pSystem = GetComponent<ParticleSystem> ();
		emitRate = 30;
		finished = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (finished && !pSystem.IsAlive()) {
			Destroy (transform.parent.gameObject);
		}
	}

	public void Emit() {
		pSystem.Play ();
		pSystem.Emit (emitRate);
	}

	public void RemoveWhenFinished() {
		finished = true;
	}
}

