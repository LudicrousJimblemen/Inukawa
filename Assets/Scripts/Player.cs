using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

// TODO: This entire class is bad.
public static class Player {
	public static EntityHuman Human;

	// TODO: Remove console
	public static void Parse(string input, Console console) {
		input = input.ToLowerInvariant();
		input = Regex.Replace(input, @"[^a-z0-9-'\s]", String.Empty);
		List<string> processed = Regex.Split(input, @"\s+").ToList();
		List<Token> tokens = new List<Token>();

		for (int i = 0; i < processed.Count; i++) {
			tokens.Add(new Token());
			tokens[i].String = processed[i];

			foreach (var entity in World.Entities) {
				int extraLength;

				if (entity.Identity != null) {
					extraLength = entity.Identity.Cases.NominativeSingular.Split(' ').Length - 1;
					if (i + extraLength < processed.Count) {
						if (processed.Range(i, i + extraLength + 1).Flatten() == entity.Identity.Cases.NominativeSingular) {
							tokens[i].Possibilities.Add(new Possibility(entity, extraLength, false));
							i += extraLength;
							continue;
						}
					}
					extraLength = entity.Identity.Cases.NominativePlural.Split(' ').Length - 1;
					if (i + extraLength < processed.Count) {
						if (processed.Range(i, i + extraLength + 1).Flatten() == entity.Identity.Cases.NominativePlural) {
							tokens[i].Possibilities.Add(new Possibility(entity, extraLength, true));
							i += extraLength;
							continue;
						}
					}
				}

				extraLength = entity.Cases.NominativeSingular.Split(' ').Length - 1;
				if (i + extraLength < processed.Count) {
					if (processed.Range(i, i + extraLength + 1).Flatten() == entity.Cases.NominativeSingular) {
						tokens[i].Possibilities.Add(new Possibility(entity, extraLength, false));
						i += extraLength;
						continue;
					}
				}

				extraLength = entity.Cases.NominativePlural.Split(' ').Length - 1;
				if (i + extraLength < processed.Count) {
					if (processed.Range(i, i + extraLength + 1).Flatten() == entity.Cases.NominativePlural) {
						tokens[i].Possibilities.Add(new Possibility(entity, extraLength, true));
						i += extraLength;
						continue;
					}
				}
			}
		}
		
		console.Write(processed.Flatten() + "\n" + tokens.Select(x => x.ToString()).Flatten(", "));
	}

	private struct Possibility {
		private Entity Entity;
		private int Length;
		private bool Plural;

		public Possibility(Entity entity, int length, bool plural) {
			this.Entity = entity;
			this.Length = length;
			this.Plural = plural;
		}

		// TODO: Remove
		public override string ToString() {
			return "<color=\"#ff8800\">(" + Entity + " " + Length + " " + (Plural ? "plural" : "singular") + ")</color>";
		}
	}

	private class Token {
		public string String;
		public List<Possibility> Possibilities = new List<Possibility>();

		// TODO: Remove
		public override string ToString() {
			return Possibilities.Any() ? "<color=\"#ff0000\">" + String + "</color> " + String.Join(" ", Possibilities.Select(x => x.ToString()).ToArray()) : String;
		}
	}
}