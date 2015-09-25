using System;
public struct Loc2D
{
	public int x, y;
	public Loc2D(int x, int y) {
		this.x = x;
		this.y = y;
	}

	public override string ToString() {
		return "x: " + x + ", y: " + y;
	}
}

