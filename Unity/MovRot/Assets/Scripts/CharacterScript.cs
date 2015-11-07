using UnityEngine;
using System.Collections;

public class CharacterScript : MonoBehaviour {

	public float moveSpeed = 1f;
	public GridManager gridManager;
	public Animator anim;

	private Loc2D gridLoc;
	public Loc2D GridLoc { get { return gridLoc; } set {gridLoc = value; } }
	private float currentSpeed;
	private bool isMoving = false;
	private float timer = 0f;
	private Vector3 moveDir = Vector3.zero;
	private Loc2D gridTarget;
	private float animTime;

	private GameControllerScript gameController;
	private MoveScript moveScript;

	// Use this for initialization
	void Start () {
		gridLoc = gridManager.PosToGrid (transform.localPosition);
		animTime = 1f / moveSpeed;
		gameController = GameObject.FindObjectOfType<GameControllerScript> ();
		moveScript = GetComponent<MoveScript> ();
	}
	
	// Update is called once per frame
	void Update () {

		float moveVertical = Input.GetAxis ("Vertical");
		float moveHorizontal = Input.GetAxis ("Horizontal");
		bool rotateLeft = Input.GetMouseButton(0);
		bool rotateRight = Input.GetMouseButton(1);

		if (moveScript.moving) {
			anim.SetFloat ("speed", 1f);
		} else {
			anim.SetFloat ("speed", 0f);
		}

		if (gridManager.IsRotatingAnim ()) {
			//Do nothing
		} else if (moveVertical != 0) {
			int dy = moveVertical > 0 ? 1 : -1;
			Loc2D target = GridLoc.WithY (dy);
			if (gridManager.GetTile (target) != null) {
				moveScript.MoveTo (target, gridManager);
				transform.LookAt (new Vector3 (transform.position.x, transform.position.y, transform.position.z + dy));
			} else if (gridManager.GetTile (target.WithY (dy)) != null) {
				moveScript.JumpTo (target.WithY (dy), gridManager);
				transform.LookAt (new Vector3 (transform.position.x, transform.position.y, transform.position.z + dy));
			}
		} else if (moveHorizontal != 0) {
			int dx = moveHorizontal > 0 ? 1 : -1;
			Loc2D target = GridLoc.WithX (dx);
			if (gridManager.GetTile (target) != null) {
				moveScript.MoveTo (target, gridManager);
				transform.LookAt (new Vector3 (transform.position.x + dx, transform.position.y, transform.position.z));
			} else if (gridManager.GetTile (target.WithX (dx))) {
				moveScript.JumpTo (target.WithX (dx), gridManager);
				transform.LookAt (new Vector3 (transform.position.x + dx, transform.position.y, transform.position.z));
			}
		} else if (!gameController.IsGameOver) {
			if (rotateLeft) {
				gridManager.RotateAbout(gridLoc, Direction.LEFT);
			} else if (rotateRight) {
				gridManager.RotateAbout(gridLoc, Direction.RIGHT);
			}
		}
	}
}
