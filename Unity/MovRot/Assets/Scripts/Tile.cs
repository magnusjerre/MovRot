using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {
	
	private Loc2D gridLoc;
	public Loc2D GridLoc  {
		set { this.gridLoc = value; }
		get { return this.gridLoc; }
	}
	
	public bool IsStatic = false;


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override string ToString ()
	{
		return "gridX: " + gridLoc.x + ", gridY: " + gridLoc.y;
	}
}
