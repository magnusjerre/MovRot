using UnityEngine;
using System.Collections;

public interface GridElement {
	void GridLoc(Loc2D loc);
	Loc2D GridLoc();
	GridManager Grid();
}
