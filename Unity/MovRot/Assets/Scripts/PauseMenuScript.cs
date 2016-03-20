using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class PauseMenuScript : MonoBehaviour
{
	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public void RestartLevel() {
		Scene scene = SceneManager.GetActiveScene ();
		SceneManager.LoadScene (scene.buildIndex);
		Time.timeScale = 1f;
	}

	public void LoadMenu() {
		SceneManager.LoadScene (0);
		Time.timeScale = 1f;
	}
}

