using UnityEngine;
using System.Collections;

public class CharacterScript : MonoBehaviour, GridElement {
	
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

		if (gridManager.IsRotatingAnim () || moveScript.moving || moveScript.jumping) {
			//Do nothing
		} else if (moveVertical != 0) {
			int dy = moveVertical > 0 ? 1 : -1;
			Loc2D target = GridLoc().WithY (dy);
			if (gridManager.GetTile (target) != null) {
				moveScript.MoveTo (target);
				transform.LookAt (new Vector3 (transform.position.x, transform.position.y, transform.position.z + dy));
			} else if (gridManager.GetTile (target.WithY (dy)) != null) {
				moveScript.JumpTo (target.WithY (dy));
				transform.LookAt (new Vector3 (transform.position.x, transform.position.y, transform.position.z + dy));
			}
		} else if (moveHorizontal != 0) {
			int dx = moveHorizontal > 0 ? 1 : -1;
			Loc2D target = GridLoc().WithX (dx);
			if (gridManager.GetTile (target) != null) {
				moveScript.MoveTo (target);
				transform.LookAt (new Vector3 (transform.position.x + dx, transform.position.y, transform.position.z));
			} else if (gridManager.GetTile (target.WithX (dx))) {
				moveScript.JumpTo (target.WithX (dx));
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

	#endregion
}
