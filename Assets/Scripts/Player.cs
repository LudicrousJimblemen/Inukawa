using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

public static class Player {
	public static Entity Entity;

	public static void Parse(string input) {
		// e.g.: I want to go to  Zimbabwe today!! Who's with me?
		// to: ["i", "want", "to", "go", "to", "zimbabwe", "today", "who's", "with", "me"]
		string[] processed = Regex.Split(Regex.Replace(input.ToLowerInvariant(), @"[^A-Za-z0-9-'\s]", String.Empty), @"\s+");

		List<Entity> accessible = World.Entities.Where(x => x.Accessible(Entity)).ToList();
		// UnityEngine.Debug.Log(String.Join(" ", accessible.Select(x => x.Cases.NominativeSingular).ToArray()));

		foreach (var entity in accessible.Where(x => x.Identity != null)) {

		}
	}
}