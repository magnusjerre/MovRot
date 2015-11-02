using System;
public class Timer
{
	private bool condition = false;
	public float elapsedTime, timer;
	private Listener listener;

	public Timer (Listener listener, float timer)
	{
		this.listener = listener;
		this.timer = timer;
		Reset ();
	}

	public void Update(float dt) {
		if (condition) {
			elapsedTime += dt;
			if (elapsedTime > timer) {
				listener.Notify(this);
			}
		}
	}

	public void Reset() {
		elapsedTime = 0f;
		condition = false;
	}

	public void Start() {
		condition = true;
	}
}