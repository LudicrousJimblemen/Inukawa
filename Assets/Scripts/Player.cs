using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

/*

	Notes:
		baby1: dog1
		baby2: dog2
		dog1:
		dog2: egg1
		cat1: egg2
		egg1:
		egg2:
		sandwich1:

		> (tokenize) <
		kick the baby's dog's egg
		kick the [baby1, baby2]> dog's egg
		kick the [baby1, baby2]> [dog1, dog2]> egg
		kick the [baby1, baby2]> [dog1, dog2]> [egg1, egg2]
		> ([egg1, egg2] where owner is in [dog1, dog2]) <
		kick the [baby1, baby2]> [dog1, dog2]> [egg1]
		> ([dog1, dog2] where all of [egg1] is possession) <
		kick the [baby1, baby2]> [dog2]> [egg1]
		> ([dog2] where owner is in [baby1, baby2]) <
		kick the [baby1, baby2]> [dog2]> [egg1]
		> ([baby1, baby2] where all of [dog2] is possession) <
		kick the [baby2]> [dog2]> [egg1]
		== SUCCESS ==

		> (tokenize) <
		kick the baby's cat's egg
		kick the [baby1, baby2]> cat's egg
		kick the [baby1, baby2]> [cat1]> egg
		kick the [baby1, baby2]> [cat1]> [egg1, egg2]
		> ([egg1, egg2] where owner is in [cat1]) <
		kick the [baby1, baby2]> [cat1]> [egg2]
		> ([cat1] where all of [egg2] is possession) <
		kick the [baby1, baby2]> [cat1]> [egg2]
		> ([cat1] where owner is in [baby1, baby2]) <
		kick the [baby1, baby2]> []> [egg2]
		== ERROR ==
		
		> (tokenize) <
		kick the baby's sandwich
		kick the [baby1, baby2]> sandwich
		kick the [baby1, baby2]> [sandwich1]
		> ([sandwich1] where owner is in [baby1]) <
		kick the [baby1, baby2]> []
		== ERROR ==
*/

public static class Player {
	public static EntityHuman Human;

	// TODO: Remove console
	public static ParseResult Parse(string input, Console console) {
		input = input.ToLowerInvariant();
		input = Regex.Replace(input, @"[^a-z0-9-'\s]", String.Empty);
		string[] processed = Regex.Split(input, @"\s+").Where(x => x != "a" && x != "an" && x != "the").ToArray();

		console.Write(processed.Flatten(" "));

		List<Token> tokens = new List<Token>();

		// tokenize
		for (int i = 0; i < processed.Length; i++) {
			Token currentToken = new Token();
			tokens.Add(currentToken);
			currentToken.String = processed[i];
			foreach (var entity in World.Entities) {
				if (entity.Identity != null) {
					int caseLength = entity.Identity.Cases.WordCount;

					if (i + caseLength - 1 < processed.Length) {
						string flattened = processed.TakeFrom(i, caseLength).Flatten(" ");

						if (entity.Identity.Cases.All.Contains(flattened)) {
							currentToken.String = flattened;
							currentToken.Found = true;
							currentToken.PreviousEntityMatches.Add(entity);
							currentToken.EntityMatches.Add(entity);
							i += caseLength - 1;
						}
						if (flattened == entity.Identity.Cases.GenitiveSingular || flattened == entity.Identity.Cases.GenitivePlural) {
							currentToken.Genitive = true;
						}
						if (flattened == entity.Identity.Cases.NominativePlural || flattened == entity.Identity.Cases.GenitivePlural) {
							currentToken.Plural = true;
						}
					}
				} else {
					int caseLength = entity.Cases.WordCount;

					if (i + caseLength - 1 < processed.Length) {
						string flattened = processed.TakeFrom(i, caseLength).Flatten(" ");

						if (entity.Cases.All.Contains(flattened)) {
							currentToken.String = flattened;
							currentToken.Found = true;
							currentToken.PreviousEntityMatches.Add(entity);
							currentToken.EntityMatches.Add(entity);
							i += caseLength - 1;
						}
						if (flattened == entity.Cases.GenitiveSingular || flattened == entity.Cases.GenitivePlural) {
							currentToken.Genitive = true;
						}
						if (flattened == entity.Cases.NominativePlural || flattened == entity.Cases.GenitivePlural) {
							currentToken.Plural = true;
						}
					}
				}
			}
		}
		
		// "from" --> genitive
		while (tokens.Any(x => x.String == "from")) {
			int fromIndex = tokens.FindIndex(x => x.String == "from");

			tokens.RemoveAt(fromIndex);

			int genitiveIndexStart = fromIndex;
			int genitiveIndexCount = 1;

			while (genitiveIndexStart + genitiveIndexCount < tokens.Count && tokens[genitiveIndexStart + genitiveIndexCount].Found) {
				genitiveIndexCount++;
			}
			tokens[genitiveIndexStart + genitiveIndexCount - 1].Genitive = true;

			int nominativeIndexStart = fromIndex - 1;
			int nominativeIndexCount = 1;

			while (nominativeIndexStart - 1 >= 0 && tokens[nominativeIndexStart - 1].Found) {
				nominativeIndexStart--;
				nominativeIndexCount++;
			}

			tokens = tokens.Take(nominativeIndexStart).Concat(
				tokens.Skip(genitiveIndexStart).Take(genitiveIndexCount)).Concat(
					tokens.Skip(nominativeIndexStart).Take(nominativeIndexCount)).ToList();
		}
		
		List<Token> previous = null;

		while (previous != tokens) {
			previous = tokens;

			for (int i = tokens.Count - 1; i >= 0; i--) {
				if (tokens[i].Genitive) {
					if (i + 1 < tokens.Count) {
						tokens[i + 1].EntityMatches =
							tokens[i + 1]
							.EntityMatches
							.Where(x => tokens[i].EntityMatches.Any(y => y.Has(x)))
							.ToList();

						if (!tokens[i + 1].EntityMatches.Any()) {
							return new ParseResult(tokens, ParseResultType.ErrorInvalidGenitive, i + 1);
						}

						tokens[i].EntityMatches =
							tokens[i]
							.EntityMatches
							.Where(x => tokens[i + 1].EntityMatches.All(y => x.Has(y)))
							.ToList();
					} else {
						return new ParseResult(tokens, ParseResultType.ErrorUnpairedGenitive, i);
					}
				}

				foreach (var token in tokens) {
					token.PreviousEntityMatches = token.EntityMatches;
				}
			}
		}

		foreach (var token in tokens) {
			if (token.EntityMatches.Count > 1) {
				if (token.EntityMatches.Any(x => Human.Has(x))) {
					token.PreviousEntityMatches = token.EntityMatches;
					token.EntityMatches = token.EntityMatches.Where(x => Human.Has(x)).ToList();
				}
			}
		}

		if (tokens.Any(x => x.EntityMatches.Count > 1 && !x.Plural)) {
			return new ParseResult(tokens, ParseResultType.ErrorAmbiguousToken, tokens.FindIndex(x => x.EntityMatches.Count > 1 && !x.Plural));
		}

		// TODO: Remove
		console.Write(String.Join(" ", tokens.Select(token => String.Format(
			"<color=\"#{0}\">{1}</color>{2}",
			token.Found ? "ffff00" : "fff",
			token.String,
			token.Found ? " [" + String.Join(", ", token.EntityMatches.Select(x => String.Format(
				"<color=\"#{0}\">{1}{2}</color>",
				token.Genitive ? "00ffff" : "ff8800",
				x,
				x.Identity == null ? String.Empty : "(" + x.Identity.Name + ")")).ToArray()) + "]" : String.Empty)).ToArray()));

		return new ParseResult(tokens, ParseResultType.Success, -1);
	}
}