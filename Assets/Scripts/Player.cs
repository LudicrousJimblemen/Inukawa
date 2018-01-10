using System;
using System.Text.RegularExpressions;

public static class Player {
	public static Entity Entity;

	public static void Parse(string input) {
		// e.g.: I want to go to  Zimbabwe today!! Who's with me?
		// to: ["i", "want", "to", "go", "to", "zimbabwe", "today", "who's", "with", "me"]
		string[] processed = Regex.Split(Regex.Replace(input.ToLowerInvariant(), @"[^A-Za-z0-9-'\s]", String.Empty), @"\s+");

		UnityEngine.Debug.Log(processed);
	}
}