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
		moveScript.AddListener (this);
	}

	public void Move(float moveVertical, float moveHorizontal) {
		if (gridManager.IsRotatingAnim () || moveScript.isMoving || moveScript.isJumping || moveScript.isFalling) {
            if (moveScript.isLanding)
            {
                anim.SetBool("jump", false);
                moveScript.isLanding = false;
                moveScript.isJumping = false;
            }
            if (moveScript.startFalling)
            {
                anim.SetTrigger("fall");
                moveScript.RegisterFallAnim();
            }
            return;
		}

		if (moveVertical != 0) {
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
		}
        
		if (moveScript.isMoving) {
			anim.SetFloat ("speed", 1f);
		} else {
			anim.SetFloat ("speed", 0f);
		}

        if (moveScript.isJumping)
        {
            anim.SetBool("jump", true);
        }
    }

	public void RotateClockwise(bool clockwise) {
		if (gridManager.IsRotatingAnim () || moveScript.isMoving || moveScript.isJumping || moveScript.isFalling) {
			return;
		}

		if (clockwise) {
			gridManager.RotateAbout(gridLoc, Direction.RIGHT);
		} else {
			gridManager.RotateAbout(gridLoc, Direction.LEFT);
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
		gameController.DisplayGameOver ();
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
