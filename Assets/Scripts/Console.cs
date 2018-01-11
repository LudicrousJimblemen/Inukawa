using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class Console : MonoBehaviour {
	private Text text;

	private void Awake() {
		this.text = GetComponent<Text>();
	}

	public void Write(string input) {
		this.text.text += "\n\n" + input;
	}
}