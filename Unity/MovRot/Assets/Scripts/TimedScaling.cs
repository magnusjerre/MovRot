using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class TimedScaling : MonoBehaviour
{

	List<Transform> transforms;
	public List<float> scales;
	public List<float> timer;
	private List<Vector3> initScales;	//initials scales, udpated after each change in position from timer array


	private bool running;
	private int pos;
	private float time, elapsed;
	private float targetScale;

	void Start() {
		transforms = new List<Transform> ();
		initScales = new List<Vector3> ();
		if (scales.Count != timer.Count || scales.Count == 0) {
			throw new UnityException ("Mismatch between number of scales and timers");
		}
		running = false;
		pos = 0;
		time = timer [0];
		targetScale = scales [0];
	}

	void Update() {
		if (running) {
			elapsed += Time.deltaTime;
			if (elapsed > time) {
				if (pos == scales.Count - 1) {
					running = false;
					gameObject.SetActive (false);
				} else {
					elapsed = 0f;
					pos++;
					time = timer [pos];
					targetScale = scales [pos];
				}
			}

			float delta = elapsed / time;
			for (int i = 0; i < transforms.Count; i++) {
				float newSX = Mathf.Lerp (initScales [i].x, targetScale, delta);
				float newSY = Mathf.Lerp (initScales [i].y, targetScale, delta);
				float newSZ = Mathf.Lerp (initScales [i].z, targetScale, delta);
				transforms [i].localScale = new Vector3 (newSX, newSY, newSZ);
			}
		}
	}

	public void Add(Transform t) {
		if (!transforms.Contains (t)) {
			transforms.Add (t);
			initScales.Add (t.localScale);
		}
	}

	public void RunScaling() {
		running = true;
	}


}

