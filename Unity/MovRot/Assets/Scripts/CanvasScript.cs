using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CanvasScript : MonoBehaviour {

	public Image background;
	public Text gameOverText;

	private float opacity = 0.5f;

	// Use this for initialization
	void Start () {
		background.color = new Color(background.color.r, background.color.g, background.color.b, 0f);
		gameOverText.color = new Color (gameOverText.color.r, gameOverText.color.g, gameOverText.color.b, 0f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Fade(float fadeTime) {
		StartCoroutine (FadeBackground (fadeTime));
		StartCoroutine (FadeText (gameOverText, fadeTime));
	}

	IEnumerator FadeBackground(float fadeTime) {
		float delta = opacity / (fadeTime * 60);
		while (background.color.a < opacity) {
			background.color = new Color(background.color.r, background.color.g, background.color.b, background.color.a + delta);
			yield return null;
		}
		yield return null;
	}

	IEnumerator FadeText(Text text, float fadeTime) {
		float delta = 1f / (fadeTime * 60);
		while (text.color.a < 1f) {
			text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + delta);
			yield return null;
		}
		yield return null;
	}
}
