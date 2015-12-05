using UnityEngine;
using System;

public class Timer : MonoBehaviour
{
	private bool condition = false;
	public float elapsedTime = 0f, timer = 1f, delta = 0.1f;
	public Listener listener;

	void Start() {
	}

	void Update() {
		if (condition) {
			elapsedTime += Time.deltaTime;
			if (elapsedTime > timer) {
				listener.Notify(this);
			}
		}
	}

	public void Reset() {
		elapsedTime = 0f;
		condition = false;
	}

	public void StartTimer() {
		condition = true;
	}

	public void Abort() {
		if (elapsedTime + delta > timer) {
			listener.Notify(this);
		}
		Reset ();
	}
}