using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class Console : MonoBehaviour {
	private Text text;

	private void Start() {
		this.text = GetComponent<Text>();
	}

	private void Write(string text) {
		this.text.text += "\n\n" + text;
	}
}