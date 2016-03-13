using UnityEngine;
using System.Collections;

public class Tree : MonoBehaviour, IElementAffectable
{
	public GameObject treeStem;
	public GameObject treeIce;
	public GameObject treePearl;
	Material treeMat, treeIceMat, treePearlMat;

	Color treeNormalColor, treeBurnColor, treeIceColor;
	Color treeIceNormalColor;
	Color treePearlNormalColor, treePearlBurnColor, treePearlIceColor;
	Color treePearlEmission;

	public float emissionTime = 2f;
	float elapsedTime = 0f;
	int sign = 1;

	void Awake() {
		treeMat = treeStem.GetComponent<Renderer> ().material;
		treePearlMat = treePearl.GetComponent<Renderer> ().material;

		treePearlMat.EnableKeyword ("_EMISSION");
		
		treeNormalColor = new Color (0.796f, 0.443f, 0f, 1f);
		treeBurnColor = new Color (0.278f, 0.196f, 0.090f, 1f);
		treeIceColor = new Color (1f, 0.894f, 0.757f, 1f);
		
		treePearlNormalColor = new Color (1f, 0f, 1f, 1f);
		treePearlIceColor = new Color (1f, 0.831f, 1f, 1f);
		treePearlBurnColor = new Color (1f, 0f, 0.435f, 1f);
		
		treeIceNormalColor = new Color (0.933f, 1f, 1f, 1f);
		
		treePearlEmission = new Color (0f, 0.753f, 1f);
	}


	// Use this for initialization
	void Start ()
	{
		doNormal ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		//Counts either up or down
		if (sign == 1) {
			elapsedTime += Time.deltaTime;
			if (elapsedTime > emissionTime) {
				sign = -1;
			}
		} else {
			elapsedTime -= Time.deltaTime;
			if (elapsedTime < 0f) {
				sign = 1;
			}
		}

		treePearlMat.SetColor ("_EmissionColor", treePearlEmission * (elapsedTime / emissionTime));
	}

	void doBurn ()
	{
		treeMat.color = treeBurnColor;
		treePearlMat.color = treePearlBurnColor;
		treeIce.SetActive (false);
	}
	
	void doNormal ()
	{
		treeMat.color = treeNormalColor;
		treePearlMat.color = treePearlNormalColor;
		treeIce.SetActive (false);
	}
	
	void doIce ()
	{
		treeMat.color = treeIceColor;
		treePearlMat.color = treePearlIceColor;
		treeIce.SetActive (true);
	}

	#region IElementAffectable implementation

	public void doAffect(Element element) {
		if (element == Element.FIRE) {
			doBurn ();
		} else if (element == Element.ICE) {
			doIce ();
		} else {
			doNormal();
		}
	}

	#endregion
}

