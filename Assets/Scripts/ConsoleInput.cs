using System;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class ConsoleInput : MonoBehaviour {
	public InputField InputField;
	public Text OutputText;

	public void Start() {
		InputField.onEndEdit.AddListener(x => Process(x));
	}

	private Regex removerizer = new Regex(@"[^A-Z0-9]");
	private Regex splitter = new Regex(@"\s+");

	public void Process(string input) {
		Output("> " + input);
		InputField.text = String.Empty;
		
		string[] processed = splitter
			.Split(input)
			.Select(s => removerizer.Replace(s.ToUpperInvariant(), String.Empty))
			.ToArray();

		Output(String.Join(", ", processed));
	}

	public void Output(string output) {
		OutputText.text += "\n" + output;
	}
}
