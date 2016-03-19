using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MaterialChanger : MonoBehaviour
{

	public Renderer renderer;
	public List<Material> materials;
	private int currentMatPos;

	// Use this for initialization
	void Start ()
	{
		currentMatPos = 0;
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void NextMaterial() {
		currentMatPos++;
		if (currentMatPos >= materials.Count) {
			currentMatPos = 0;
		}
		renderer.material = materials [currentMatPos];
	}

}

