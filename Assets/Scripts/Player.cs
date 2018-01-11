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
							tokens[i].AddPossibility(entity, false, false);
							tokens[i].String = processed.Range(i, i + extraLength + 1).Flatten();
							i += extraLength;
							continue;
						}
					}

					extraLength = entity.Identity.Cases.NominativePlural.Split(' ').Length - 1;
					if (i + extraLength < processed.Count) {
						if (processed.Range(i, i + extraLength + 1).Flatten() == entity.Identity.Cases.NominativePlural) {
							tokens[i].AddPossibility(entity, true, false);
							tokens[i].String = processed.Range(i, i + extraLength + 1).Flatten();
							i += extraLength;
							continue;
						}
					}
				}

				extraLength = entity.Cases.NominativeSingular.Split(' ').Length - 1;
				if (i + extraLength < processed.Count) {
					if (processed.Range(i, i + extraLength + 1).Flatten() == entity.Cases.NominativeSingular) {
						tokens[i].AddPossibility(entity, false, false);
						tokens[i].String = processed.Range(i, i + extraLength + 1).Flatten();
						i += extraLength;
						continue;
					}
				}

				extraLength = entity.Cases.NominativePlural.Split(' ').Length - 1;
				if (i + extraLength < processed.Count) {
					if (processed.Range(i, i + extraLength + 1).Flatten() == entity.Cases.NominativePlural) {
						tokens[i].AddPossibility(entity, true, false);
						tokens[i].String = processed.Range(i, i + extraLength + 1).Flatten();
						i += extraLength;
						continue;
					}
				}

				extraLength = entity.Cases.GenitiveSingular.Split(' ').Length - 1;
				if (i + extraLength < processed.Count) {
					if (processed.Range(i, i + extraLength + 1).Flatten() == entity.Cases.GenitiveSingular) {
						tokens[i].AddPossibility(entity, false, true);
						tokens[i].String = processed.Range(i, i + extraLength + 1).Flatten();
						i += extraLength;
						continue;
					}
				}

				extraLength = entity.Cases.GenitivePlural.Split(' ').Length - 1;
				if (i + extraLength < processed.Count) {
					if (processed.Range(i, i + extraLength + 1).Flatten() == entity.Cases.GenitivePlural) {
						tokens[i].AddPossibility(entity, true, true);
						tokens[i].String = processed.Range(i, i + extraLength + 1).Flatten();
						i += extraLength;
						continue;
					}
				}
			}
		}
		
		// TODO: Remove
		console.Write(processed.Flatten() + "\n" + tokens.Flatten(", "));
	}

	private struct Possibility {
		private Entity Entity;
		private bool Plural;
		private bool Possessive;

		public Possibility(Entity entity, bool plural, bool possessive) {
			this.Entity = entity;
			this.Plural = plural;
			this.Possessive = possessive;
		}

		// TODO: Remove
		public override string ToString() {
			return String.Format(
				"<color=\"#{0}\">({1}, {2}, {3})</color>",
				this.Possessive ? "0088ff" : "ff8800",
				Entity,
				Plural ? "plural" : "singular");
		}
	}

	private class Token {
		public string String;
		public List<Possibility> Possibilities = new List<Possibility>();

		public void AddPossibility(Entity entity, bool plural, bool possessive) {
			Possibilities.Add(new Possibility(entity, plural, possessive));
		}

		// TODO: Remove
		public override string ToString() {
			if (Possibilities.Any()) {
				return String.Format(
					"<color=\"#ffff00\">{0}</color> [{1}]",
					String,
					Possibilities.Flatten(", "));
			} else {
				return this.String;
			}
		}
	}
}