using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TargetScript : MonoBehaviour {

	private List<TargetScript> directDependees = new List<TargetScript>();	//targets that are dependent on this target being visited
	public void AddDependee(TargetScript target) {
		directDependees.Add (target);
	}
	public TargetScript[] directDependants;	//Targets that this target is dependent on

	private CharacterScript characterScript;
	private TargetManager targetManager;

	private bool completed = false;
	public bool Completed {
		get { return completed; }
	}
	private bool reqMet = false;
	public bool ReqMet {
		get { return reqMet; }
	}


    public List<Renderer> diamondRenderers;
    public List<Color> diamondEmissionColors;
    private ParticleSystem pSystem;

    Color diamondEmission = new Color(0f, 0.753f, 1f);

    public float emissionTime = 2f;
    float elapsedTime = 0f;
    int sign = 1;

    // Use this for initialization
    protected void Start () {

        foreach (Renderer renderer in diamondRenderers)
        {
            renderer.material.EnableKeyword("_EMISSION");
        }

        characterScript = GameObject.FindGameObjectWithTag ("Player").GetComponent<CharacterScript>();
        targetManager = GameObject.FindGameObjectWithTag ("TargetManager").GetComponent<TargetManager>();
		targetManager.GenerateDepencies (this);
        NotifyCompleted(this);
        pSystem = GetComponentInChildren<ParticleSystem>();
        pSystem.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	    if (ReqMet && !Completed)
        {
            if (sign == 1)
            {
                elapsedTime += Time.deltaTime;
                if (elapsedTime > emissionTime)
                {
                    sign = -1;
                }
            }
            else {
                elapsedTime -= Time.deltaTime;
                if (elapsedTime < 0f)
                {
                    sign = 1;
                }
            }

            for (int i = 0; i < diamondRenderers.Count; i++)
            {
                diamondRenderers[i].material.SetColor("_EmissionColor", diamondEmissionColors[i] * (elapsedTime / emissionTime));
            }
        }
	}

	public void OnTriggerEnter(Collider other) {
        Debug.Log("other: " + other);
        Debug.Log("other.gameObject: " + other.gameObject);
        Debug.Log("characterScript: " + characterScript);
        Debug.Log("characterScript.gameObject: " + characterScript.gameObject);
		if (other.gameObject == characterScript.gameObject) {
			if (ReqMet && !completed) {
				completed = true;
                ShowCompleted();
				foreach (TargetScript dependee in directDependees) {
					dependee.NotifyCompleted(this);
				}
			}
		}
	}

	bool RequirementsMet() {
		foreach (TargetScript ts in directDependants) {
			if (!ts.ReqMet) {
				return false;
			}
		}
		return true;
	}

	void NotifyCompleted(TargetScript target) {
		if (reqMet = RequirementsMet ()) {
            ShowRequirementsMet();
			Debug.Log ("Thank you dependant " + target + ". Now my (" + this + ") requirements are also met, therefore I can be visited for completion");
			Debug.Log ("Do something to signal it...");
		} else {
            ShowRequirementsNotMet();
			Debug.Log ("Thank you dependant " + target + ". I'm (" + this + ") still waiting for the other targets before completion though.");
		}
	}

    public void ShowCompleted()
    {
        Debug.Log("Show completed: " + pSystem.gameObject.activeSelf);
        pSystem.gameObject.SetActive(true);
        foreach (Renderer renderer in diamondRenderers)
        {
            renderer.material.DisableKeyword("_EMISSION");
        }
    }

    public void ShowRequirementsMet()
    {
        foreach (Renderer renderer in diamondRenderers)
        {
            renderer.gameObject.SetActive(true);
        }
    }

    public virtual void ShowRequirementsNotMet()
    {
        foreach (Renderer renderer in diamondRenderers)
        {
            renderer.gameObject.SetActive(false);
        }
    }
}
