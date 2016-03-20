using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CanvasScript : MonoBehaviour {

	public Image background;
	public Text resultMainText;
	public Button restartButton, menuButton;

	private float opacity = 0.5f;

	// Use this for initialization
	void Start () {
		background.color = new Color(background.color.r, background.color.g, background.color.b, 0f);
		resultMainText.color = new Color (resultMainText.color.r, resultMainText.color.g, resultMainText.color.b, 0f);
		Color c = restartButton.image.color;
		restartButton.image.color = new Color (c.r, c.g, c.b, 0f);
		c = menuButton.image.color;
		menuButton.image.color = new Color (c.r, c.g, c.b, 0f);
		Color buttonTextColor = restartButton.GetComponentInChildren<Text> ().color;
		restartButton.GetComponentInChildren<Text> ().color = new Color (buttonTextColor.r, buttonTextColor.g, buttonTextColor.b, 0);
		buttonTextColor = menuButton.GetComponentInChildren<Text> ().color;
		menuButton.GetComponentInChildren<Text> ().color = new Color (buttonTextColor.r, buttonTextColor.g, buttonTextColor.b, 0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Fade(string text, float fadeTime, bool fadeBackground) {
		resultMainText.text = text;
		StartCoroutine (FadeText (resultMainText, fadeTime));
		StartFadeButton (restartButton, fadeTime);
		StartFadeButton (menuButton, fadeTime);
		if (fadeBackground) {
			StartCoroutine (FadeBackground (fadeTime));
		}
	}

	public void Fade(float fadeTime) {
		StartCoroutine (FadeBackground (fadeTime));
		StartCoroutine (FadeText (resultMainText, fadeTime));
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

	IEnumerator FadeImage(Image image, float fadeTime) {
		float delta = 1f / (fadeTime * 60);
		while (image.color.a < 1f) {
			image.color = new Color (image.color.r, image.color.g, image.color.b, image.color.a + delta);
			yield return null;
		}
		yield return null;
	}

	void StartFadeButton(Button button, float fadeTime) {
		StartCoroutine (FadeImage (button.image, fadeTime));
		StartCoroutine (FadeText (button.GetComponentInChildren<Text>(), fadeTime));
	}
		
}
