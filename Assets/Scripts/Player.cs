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

		// Step 1: Tokenize

		List<Token> tokens = new List<Token>();

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
							currentToken.EntityMatches.Add(entity);
							currentToken.String = flattened;
							i += caseLength - 1;
						}
						if (flattened == entity.Identity.Cases.GenitiveSingular || flattened == entity.Identity.Cases.GenitivePlural) {
							currentToken.Genitive = true;
						}
					}
				} else {
					int caseLength = entity.Cases.WordCount;

					if (i + caseLength - 1 < processed.Length) {
						string flattened = processed.TakeFrom(i, caseLength).Flatten(" ");

						if (entity.Cases.All.Contains(flattened)) {
							currentToken.EntityMatches.Add(entity);
							currentToken.String = flattened;
							i += caseLength - 1;
						}
						if (flattened == entity.Cases.GenitiveSingular || flattened == entity.Cases.GenitivePlural) {
							currentToken.Genitive = true;
						}
					}
				}
			}
		}

		console.Write(String.Join(" ", tokens.Select(token => String.Format(
			"<color=\"#{0}\">{1}</color>{2}",
			token.EntityMatches.Any() ? "ffff00" : "fff",
			token.String,
			token.EntityMatches.Any() ? " [" + String.Join(", ", token.EntityMatches.Select(x => String.Format(
				"<color=\"#{0}\">{1}{2}</color>",
				token.Genitive ? "00ffff" : "ff8800",
				x,
				x.Identity == null ? String.Empty : "(" + x.Identity.Name + ")")).ToArray()) + "]" : String.Empty)).ToArray()));

		return new ParseResult(ParseResultType.Success);
	}
}