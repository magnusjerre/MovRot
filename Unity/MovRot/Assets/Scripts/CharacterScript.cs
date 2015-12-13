using UnityEngine;
using System.Collections;

public class CharacterScript : MonoBehaviour, GridElement, Listener {
	
	public GridManager gridManager;
	public Animator anim;

	private Loc2D gridLoc;

	private GameControllerScript gameController;
	private MoveScript moveScript;

	// Use this for initialization
	void Start () {
		gridLoc = gridManager.PosToGrid (transform.parent.localPosition);
		gameController = GameObject.FindObjectOfType<GameControllerScript> ();
		moveScript = GetComponent<MoveScript> ();
		moveScript.movementListener = this;
	}
	
	// Update is called once per frame
	void Update () {

		float moveVertical = Input.GetAxis ("Vertical");
		float moveHorizontal = Input.GetAxis ("Horizontal");
		bool rotateLeft = Input.GetMouseButton(0);
		bool rotateRight = Input.GetMouseButton(1);

		if (moveScript.isMoving) {
			anim.SetFloat ("speed", 1f);
		} else {
			anim.SetFloat ("speed", 0f);
		}

		if (gridManager.IsRotatingAnim () || moveScript.isMoving || moveScript.isJumping || moveScript.isFalling) {
			//Do nothing
		} else if (moveVertical != 0) {
			int dy = moveVertical > 0 ? 1 : -1;
			Loc2D target = GridLoc().WithY (dy);
			transform.LookAt (new Vector3 (transform.position.x, transform.position.y, transform.position.z + dy));
			if (gridManager.GetTile (target) != null && gridManager.GetTile(target).IsTraversable()) {
				if (dy == 1) {
					moveScript.MoveDirection(MoveDirection.UP, 1);
				} else {
					moveScript.MoveDirection(MoveDirection.DOWN, 1);
				}
			} else if (gridManager.GetTile(target) == null && gridManager.GetTile (target.WithY (dy)) != null && gridManager.GetTile(target.WithY(dy)).IsTraversable()) {
				if (dy == 1) {
					moveScript.MoveDirection(MoveDirection.UP, 2); 
				} else {
					moveScript.MoveDirection(MoveDirection.DOWN, 2);
				}
			}
		} else if (moveHorizontal != 0) {
			int dx = moveHorizontal > 0 ? 1 : -1;
			Loc2D target = GridLoc().WithX (dx);
			transform.LookAt (new Vector3 (transform.position.x + dx, transform.position.y, transform.position.z));
			if (gridManager.GetTile (target) != null && gridManager.GetTile(target).IsTraversable()) {
				if (dx == 1) {
					moveScript.MoveDirection(MoveDirection.RIGHT, 1);
				} else {
					moveScript.MoveDirection(MoveDirection.LEFT, 1);
				}
			} else if (gridManager.GetTile(target) == null && gridManager.GetTile (target.WithX (dx)) && gridManager.GetTile(target.WithX(dx)).IsTraversable()) {
				if (dx == 1) {
					moveScript.MoveDirection(MoveDirection.RIGHT, 2);
				} else {
					moveScript.MoveDirection(MoveDirection.LEFT, 2);
				}
			}
		} else if (!gameController.IsGameOver) {
			if (rotateLeft) {
				gridManager.RotateAbout(gridLoc, Direction.LEFT);
			} else if (rotateRight) {
				gridManager.RotateAbout(gridLoc, Direction.RIGHT);
			}
		}
	}

	#region GridElement implementation

	public void GridLoc (Loc2D loc)
	{
		gridLoc = loc;
	}

	public Loc2D GridLoc ()
	{
		return gridLoc;
	}

	public GridManager Grid ()
	{
		return gridManager;
	}

	public void NotifyTileDestroyed(Tile tile) 
	{
		transform.parent = gridManager.transform;
		moveScript.Fall ();
	}

	#endregion

	#region Listener implementation
	
	public void Notify (object o)
	{
		if (o.GetType () == typeof(MoveScript)) {
			Debug.Log ("Been notified of movement finished");
			if (gridManager.GetTile(gridLoc) != null) {
				gridManager.GetTile(gridLoc).PerformAction(ActionType.MOVEMENT);
			}
		}
	}
	
	#endregion
}
