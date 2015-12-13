using UnityEngine;
using System;

public enum MoveDirection
{
	UP, DOWN, LEFT, RIGHT, NONE
}

public static class MoveDirectionUtils {
	public static Loc2D Dir(MoveDirection moveDir) {
		if (moveDir == MoveDirection.UP) {
			return new Loc2D(0, 1);
		} else if (moveDir == MoveDirection.RIGHT){
			return new Loc2D(1, 0);
		} else if (moveDir == MoveDirection.DOWN) {
			return new Loc2D(0, -1);
		} else if (moveDir == MoveDirection.LEFT) {
			return new Loc2D(-1, 0);
		} else {
			return Loc2D.Zero();
		}
	}
}
