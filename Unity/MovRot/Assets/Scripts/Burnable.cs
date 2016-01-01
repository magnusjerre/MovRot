using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Timer))]
public class Burnable : MonoBehaviour, Listener
{

	public bool IsBurnable;
	public float TimeToBurnUp;

	public Timer timer;

	// Use this for initialization
	void Start ()
	{
		timer.listener = this;
		timer.timer = TimeToBurnUp;
	}

	public void StartBurn() {
		timer.StartTimer ();
	}

	public void PauseBurn() {
		timer.PauseTimer ();
	}

	public void StopBurn() {
		timer.Abort ();
	}

	public void ResetBurn() {
		timer.Reset ();
	}

	#region Listener implementation

	public virtual void Notify (object o)
	{
		Destroy (gameObject);
	}

	#endregion
}

