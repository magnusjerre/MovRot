using UnityEngine;
using System;
public struct Loc2D
{
	public int x, y;
	public Loc2D(int x, int y) {
		this.x = x;
		this.y = y;
	}

	public static Loc2D Zero() {
		return new Loc2D (0, 0);
	}

	public static Loc2D Default() {
		return new Loc2D (-1, -1);
	}

	public override string ToString() {
		return "x: " + x + ", y: " + y;
	}

	public Loc2D WithY(int dy) {
		return new Loc2D(x, this.y + dy);
	}

	public Loc2D WithX(int dx) {
		return new Loc2D (this.x + dx, y);
	}

	public static Vector3 Diff (Loc2D target, Loc2D start)
	{
		return new Vector3(target.x - start.x, 0, target.y - start.y);
	}
}

