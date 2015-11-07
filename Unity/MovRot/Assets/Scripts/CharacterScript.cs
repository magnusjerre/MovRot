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

	// Use this for initialization
	void Start () {
		gridLoc = gridManager.PosToGrid (transform.localPosition);
		animTime = 1f / moveSpeed;
		gameController = GameObject.FindObjectOfType<GameControllerScript> ();
	}
	
	// Update is called once per frame
	void Update () {

		float moveVertical = Input.GetAxis ("Vertical");
		float moveHorizontal = Input.GetAxis ("Horizontal");
		if (moveVertical != 0) {
			int dy = moveVertical > 0 ? 1 : -1;
			Debug.Log("(int)moveVertical: " + (moveVertical));
			GetComponent<MoveScript> ().JumpTo (GridLoc.WithY ((int)dy * 2), gridManager);
		} else if (moveHorizontal != 0) {
			int dx = moveHorizontal > 0 ? 1 : -1;
			GetComponent<MoveScript> ().JumpTo (GridLoc.WithX ((int)dx * 2), gridManager);
		}
		/*
		if (isMoving) {
			timer += Time.deltaTime;
			transform.position += moveDir * moveSpeed * Time.deltaTime;
			if (timer > animTime) {
				timer = 0f;
				isMoving = false;
				transform.localPosition = gridManager.GridToPos(gridTarget);
				gridLoc = gridTarget;
				anim.SetFloat("speed", 0f);
				gridManager.GetTile(new Loc2D((int)transform.localPosition.x, (int)transform.localPosition.z)).PerformAction(ActionType.MOVEMENT);	//Register movement with tile
				//Should add some kind of check whether the character is actually standing on something or not...
			}
		} else if (gridManager.IsRotatingAnim()) {
			//Do nothing
		} else if (!gameController.IsGameOver) {


			float moveVertical = Input.GetAxis ("Vertical");
			float moveHorizontal = Input.GetAxis ("Horizontal");
			bool rotateLeft = Input.GetMouseButton(0);
			bool rotateRight = Input.GetMouseButton(1);
			
			if (moveVertical != 0f) {
				int dy = moveVertical > 0 ? 1 : -1;
				transform.LookAt(new Vector3(transform.position.x, transform.position.y, transform.position.z + dy));
				gridTarget = new Loc2D(gridLoc.x, gridLoc.y + dy);
				moveDir = new Vector3(0, 0, dy);
				if (gridManager.IsTile(gridTarget)) {
					isMoving = true;
					anim.SetFloat("speed", 1f);
				}
			} else if (moveHorizontal != 0f) {
				int dx = moveHorizontal > 0 ? 1 : -1;
				transform.LookAt(new Vector3(transform.position.x + dx, transform.position.y, transform.position.z));
				gridTarget = new Loc2D(gridLoc.x + dx, gridLoc.y);
				moveDir = new Vector3(dx, 0, 0);
				if (gridManager.IsTile(gridTarget)) {
					isMoving = true;
					anim.SetFloat("speed", 1f);
				}
			} else if (rotateLeft) {
				gridManager.RotateAbout(gridLoc, Direction.LEFT);
			} else if (rotateRight) {
				gridManager.RotateAbout(gridLoc, Direction.RIGHT);
			}
		}
	*/
	}
}
