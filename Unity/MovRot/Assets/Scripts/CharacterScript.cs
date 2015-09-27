using UnityEngine;
using System.Collections;

public class CharacterScript : MonoBehaviour {

	public float moveSpeed = 1f;
	public GridManager gridManager;

	private Loc2D gridLoc;
	private float currentSpeed;
	private bool isMoving = false;
	private float timer = 0f;
	private Vector3 moveDir = Vector3.zero;
	private Loc2D gridTarget;
	private float animTime;

	// Use this for initialization
	void Start () {
		gridLoc = new Loc2D (2, 1);
		animTime = 1f / moveSpeed;
	}
	
	// Update is called once per frame
	void Update () {

		if (isMoving) {
			timer += Time.deltaTime;
			transform.position += moveDir * moveSpeed * Time.deltaTime;
			if (timer > animTime) {
				timer = 0f;
				isMoving = false;
				transform.localPosition = gridManager.GridToPos(gridTarget);
				gridLoc = gridTarget;
			}
		} else {
			float vertical = Input.GetAxis ("Vertical");
			float horizontal = Input.GetAxis ("Horizontal");
			if (vertical != 0f  && horizontal != 0) {
				return;	//Not legal to move diagonally
			}
			
			if (vertical != 0f) {
				int dy = vertical > 0 ? 1 : -1;
				transform.LookAt(new Vector3(transform.position.x, transform.position.y, transform.position.z + dy));
				gridTarget = new Loc2D(gridLoc.x, gridLoc.y + dy);
				moveDir = new Vector3(0, 0, dy);
				if (gridManager.IsTile(gridTarget)) {
					isMoving = true;
				}
			}

			if (horizontal != 0f) {
				int dx = horizontal > 0 ? 1 : -1;
				transform.LookAt(new Vector3(transform.position.x + dx, transform.position.y, transform.position.z));
				gridTarget = new Loc2D(gridLoc.x + dx, gridLoc.y);
				moveDir = new Vector3(dx, 0, 0);
				if (gridManager.IsTile(gridTarget)) {
					isMoving = true;
				}
			}
		}
	
	}
}
