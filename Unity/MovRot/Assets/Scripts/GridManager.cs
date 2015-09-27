using UnityEngine;
using System.Collections;

public class GridManager : MonoBehaviour {

	public int width, height;
	public float tileSize = 1.0f;
	public Transform rotateTransf;
	public float rotationTime = 0.5f;

	private Tile[,] grid;
	private Tile[] rotatingTiles;
	private Loc2D[] rotateTargets;
	private float rotateAmount = 90f;
	private float elapsedRotAmount = 0f;
	private Direction rotateDir;
	
	// Use this for initialization
	void Start () {
		grid = new Tile[width, height];
		Tile[] childTiles = GetComponentsInChildren<Tile> ();
		foreach (Tile tile in childTiles) {
			Loc2D gridLoc = PosToGrid(tile.transform.position);
			tile.GridLoc = gridLoc;
			grid[gridLoc.x, gridLoc.y] = tile;
		}
	}
	
	// Update is called once per frame
	void Update () {

		if (IsRotating()) {	//Means it's rotating
			float dRot = (Time.deltaTime / rotationTime) * rotateAmount;
			elapsedRotAmount += dRot;
			rotateTransf.Rotate(0, dRot * (int)rotateDir, 0);
			foreach (Tile tile in rotatingTiles) {
				if (tile != null)
					tile.transform.LookAt(tile.transform.position + Vector3.forward);
			}


			if (elapsedRotAmount >= rotateAmount) {
				for (int i = 0; i < rotatingTiles.Length; i++) {
					if (rotatingTiles[i] != null) {
						Tile tile = rotatingTiles[i];
						tile.transform.parent = transform;
						tile.GridLoc = rotateTargets[i];
						tile.transform.localPosition = new Vector3(rotateTargets[i].x * tileSize, 0, rotateTargets[i].y * tileSize);
						tile.transform.LookAt(tile.transform.position + Vector3.forward);
						grid[rotateTargets[i].x, rotateTargets[i].y] = tile;
					}
				}

				elapsedRotAmount = 0f;
				rotatingTiles = null;
				rotateTargets = null;
				rotateTransf.LookAt(rotateTransf.position + Vector3.forward);
			}
		}
	
	}



	public void RotateAbout(Loc2D loc, Direction dir) {
		if (CanRotate (loc)) {
			rotateDir = dir;
			rotatingTiles = GetSurroundingTiles(loc.x, loc.y);
			rotateTargets = GetTargetRotations(rotatingTiles, dir);
			foreach (Tile tile in rotatingTiles) {
				if (tile != null) {
					tile.transform.parent = rotateTransf;
					grid[tile.GridLoc.x, tile.GridLoc.y] = null;
				}
			}
		}
	}

	Loc2D[] GetTargetRotations(Tile[] tiles, Direction dir) {
		Loc2D[] targets = new Loc2D[4];
		if (tiles [0] != null) {
			targets[0] = new Loc2D((int)(tiles[0].GridLoc.x + ((int)dir) * tileSize), (int)(tiles[0].GridLoc.y - tileSize));
		}

		if (tiles [1] != null) {
			targets[1] = new Loc2D((int)(tiles[1].GridLoc.x - tileSize), (int)(tiles[1].GridLoc.y - ((int)dir) * tileSize));
		}

		if (tiles [2] != null) {
			targets[2] = new Loc2D((int)(tiles[2].GridLoc.x - ((int)dir) * tileSize), (int)(tiles[2].GridLoc.y + tileSize));
		}

		if (tiles [3] != null) {
			targets[3] = new Loc2D((int)(tiles[3].GridLoc.x + tileSize), (int)(tiles[3].GridLoc.y + ((int)dir) * tileSize));
		}

       return targets;
	}

	bool CanRotate(Loc2D loc) {
		if (loc.x < 1 || loc.x > width - 2)
			return false;
		if (loc.y < 1 || loc.y > height - 2)
			return false;
		return true;
	}

	Tile[] GetSurroundingTiles(int x, int y) {
		Tile[] tiles = new Tile[4];

		Loc2D left = new Loc2D(x - 1, y);
		Loc2D right = new Loc2D(x + 1, y);
		Loc2D up = new Loc2D (x, y + 1);
		Loc2D down = new Loc2D(x, y -1);

		//Clockwise, starting from 12 o'clock
		Tile tile = null;
		if (IsInsideGrid (up))
			tile = grid [up.x, up.y];
		tiles [0] = tile;

		tile = null;
		if (IsInsideGrid (right))
			tile = grid [right.x, right.y];
		tiles [1] = tile;

		tile = null;
		if (IsInsideGrid (down))
			tile = grid [down.x, down.y];
		tiles [2] = tile;

		tile = null;
		if (IsInsideGrid (left))
			tile = grid[left.x, left.y];
		tiles[3] = tile;

		return tiles;
	}

	bool IsInsideGrid(Loc2D loc) {
		if (loc.x < 0 || loc.x > width - 1)
			return false;
		if (loc.y < 0 || loc.y > height - 1)
			return false;
		return true;
	}

	public bool IsTile(Loc2D target) {
		if (!IsInsideGrid (target))
			return false;
		return grid [target.x, target.y] != null ? true : false;
	}

	public Vector3 GridToPos(Loc2D loc) {
		return new Vector3(loc.x * tileSize, 0, loc.y * tileSize);
	}

	public Loc2D PosToGrid(Vector3 pos) {
		return new Loc2D ((int)(pos.x / tileSize), (int)(pos.z / tileSize));
	}

	public bool IsRotating() {
		return rotatingTiles != null;
	}
}
