using UnityEngine;
using System.Collections;

public class MoveScript : MonoBehaviour, Listener
{
	
	Loc2D direction;
	
	public GridElement elementToMove;
	Tile startTile;
	
	float moveSpeed = 2f, jumpSpeed = 3f;	//per second
	
	public bool isMoving = false;
	public bool isJumping = false;
	public bool isFalling = false;
	public bool isFinished = false;
	
	private Timer timer;
	float moveTime, jumpTime;
	
	//Jumping
	private float a = -4.9f, b;
	
	public Listener movementListener;

	private Vector3 zero;

	void Awake() {
		zero = transform.localPosition;
	}

	// Use this for initialization
	void Start ()
	{
		elementToMove = GetComponent<GridElement> ();
		moveTime = 1f / moveSpeed;
		jumpTime = 1f / jumpSpeed * 2f;	//will otherwise stop halfway
		timer = GetComponent<Timer> ();
		timer.listener = this;
		timer.timer = 1f / moveSpeed;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (isMoving) {
			float dDistance = moveSpeed * Time.deltaTime;
			Vector3 pos = transform.localPosition;
			transform.localPosition = new Vector3 (pos.x + direction.x * dDistance, pos.y, pos.z + direction.y * dDistance);
		} else if (isJumping) {
			float dDistance = jumpSpeed * Time.deltaTime;
			float dt = timer.elapsedTime;
			float y = a * dt * dt + b * dt;
			Vector3 pos = transform.localPosition;
			transform.localPosition = new Vector3 (pos.x + direction.x * dDistance, y * transform.localScale.y, pos.z + direction.y * dDistance);
		} else if (isFalling) {
			float dt = timer.elapsedTime;
			float y = a * dt * dt;
			Vector3 pos = transform.localPosition;
			transform.localPosition = new Vector3 (pos.x, y * transform.localScale.y, pos.z);
		}
	}
	
	public void Fall() {
		if (isFinished || isMoving || isJumping || isFalling)
			return;
		
		isFalling = true;
		timer.timer = 2f;
		timer.StartTimer ();
	}
	
	public void MoveDirection(MoveDirection moveDirection, int distance) {
		if (isFinished || isMoving || isJumping || isFalling) 
			return;
		
		startTile = elementToMove.Grid ().GetTile (elementToMove.GridLoc ());
		this.direction = MoveDirectionUtils.Dir(moveDirection);
		if (distance == 1) {
			isMoving = true;
			isJumping = false;
			isFalling = false;
			timer.timer = moveTime; 
		} else if (distance == 2) {
			isMoving = false;
			isJumping = true;
			isFalling = false;
			b = -a * jumpTime; //b = -2ax //dy/dx of y = ax² + bx + c, 0 = 2ax + b -> b = -2ax, removing 2 because the velocity should be 0 halfway
			timer.timer = jumpTime;
		}
		timer.StartTimer ();
	}
	
	#region Listener implementation
	
	public void Notify (object o)
	{
		if (isFinished)
			return;
		
		isMoving = false;
		isJumping = false;
		timer.Reset ();
		
		//Set final positions
		Vector3 localRound = new Vector3 (Mathf.RoundToInt (transform.localPosition.x), transform.localPosition.y, Mathf.RoundToInt (transform.localPosition.z)); 
		Loc2D endPos = elementToMove.Grid ().PosToGrid (localRound + transform.parent.localPosition);
		Tile finalTile = elementToMove.Grid ().GetTile (endPos);
		if (finalTile == null) {
			if (isFalling) { //Is already falling
				isFalling = false;
				isFinished = true;
				movementListener.Notify(this);
			} else {
				Fall();
			}
		} else {
			elementToMove.GridLoc (endPos);
			transform.parent = finalTile.transform;
			transform.localPosition = zero;
			movementListener.Notify(this);
		}
	}
	
	#endregion
}

