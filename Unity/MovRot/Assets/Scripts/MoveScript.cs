using UnityEngine;
using System.Collections;

public class MoveScript : MonoBehaviour {

	public Loc2D target;
	private Vector3 targetVector;
	public Loc2D start;

	public Vector3 direction;
	
	public bool moving = false;
	public float speed = 10f;

	private CharacterScript character;

	private float diff;
	private float height = 4.9f;
	private float dy;
	private float a = -4.9f, b;

	private float timer;
	private float moveTime;
	
	public bool jumping = false;

	// Use this for initialization
	void Start () {
		character = GetComponent<CharacterScript> ();
		moveTime = 1f / speed;
		dy = height / speed;
		//dy = 4.9f;
	}
	
	// Update is called once per frame
	void Update () {
		if (moving) {
			timer += Time.deltaTime;

			if (timer > moveTime) {	//Finsihed
				transform.localPosition = targetVector;
				character.GridLoc = target;
				moving = false;
				jumping = false;
				timer = 0f;
			} else {
				transform.localPosition += direction * speed * Time.deltaTime;
			}
		}

		if (jumping) {

			if (timer > moveTime) {
				transform.localPosition = new Vector3(transform.localPosition.x, 0, transform.localPosition.z);
				jumping = false;
			} else {
				float y = -dy*timer*timer + dy * timer;
				y = a * timer * timer + b * timer;
				transform.localPosition = new Vector3(transform.localPosition.x, y, transform.localPosition.z);
			} 
		
		}
	}

	public void MoveTo(Loc2D t, GridManager gridManager) {
		if (moving)
			return;

		moving = true;

		target = t;
		targetVector = gridManager.GridToPos (target);
		Debug.Log ("targetVecyor: " + targetVector);

		start = GetComponent<CharacterScript> ().GridLoc;

		direction = Loc2D.Diff (target, start);
	}

	public void JumpTo(Loc2D t, GridManager gridManager) {
		if (jumping)
			return;

		jumping = true;
		
		GetComponent<MoveScript> ().MoveTo (t, gridManager);
		//b = -2ax
		//b = - a * (gridManager.GridToPos (t) - gridManager.GridToPos (character.GridLoc)).magnitude;
		b = - a * moveTime;

	}
}
