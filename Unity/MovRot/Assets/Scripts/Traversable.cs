using UnityEngine;
using System.Collections;

public class Traversable : MonoBehaviour, ITraversable
{

	public bool TraversableProp = true;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
	
	#region ITraversable implementation
	public bool IsTraversable ()
	{
		return TraversableProp;
	}
	#endregion
}

