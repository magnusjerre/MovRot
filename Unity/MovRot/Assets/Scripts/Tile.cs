using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

	private int gridX, gridY;
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setGridPos(int x, int y) {
		gridX = x;
		gridY = y;
	}

	public Loc2D GetGridPos() {
		return new Loc2D (gridX, gridY);
	}

	public void removeFromGrid() {
		gridX = -1;
		gridY = -1;
	}

	public override string ToString ()
	{
		return "gridX: " + gridX + ", gridY: " + gridY;
	}
}
