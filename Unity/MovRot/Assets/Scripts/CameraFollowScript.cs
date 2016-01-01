using UnityEngine;
using System.Collections;

public class CameraFollowScript : MonoBehaviour {
	
	private Transform transToFollow;
	private Vector3 offset;

	void Awake() {
		transToFollow = GameObject.FindGameObjectWithTag ("Player").transform;
		offset = transToFollow.position - transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = transToFollow.position - offset;
	}
}
