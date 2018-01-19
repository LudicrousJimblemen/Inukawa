using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

public static class InputHandler {
	public static EntityHuman Player;

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
				Cases cases = entity.Identity != null ? entity.Identity.Cases : entity.Cases;
				int caseLength = cases.WordCount;
				if(i + caseLength - 1 < processed.Length) {
					string flattened = processed.TakeFrom(i,caseLength).Flatten(" ");

					if(cases.All.Contains(flattened)) {
						currentToken.String = flattened;
						currentToken.Found = true;
						currentToken.PreviousEntityMatches.Add(entity);
						currentToken.EntityMatches.Add(entity);
						i += caseLength - 1;
					}
					if(flattened == cases.GenitiveSingular || flattened == cases.GenitivePlural) {
						currentToken.Genitive = true;
					}
					if(flattened == cases.NominativePlural || flattened == cases.GenitivePlural) {
						currentToken.Plural = true;
					}
				}
			}
		}

		// "from" --> genitive
		Func<Token,bool> genitivePredicate = (x) => { return x.String == "from" || x.String == "of";  };
		while (tokens.Any(genitivePredicate)) {
			int fromIndex = tokens.FindIndex(x => genitivePredicate(x));

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

		/* a monument to my idiocy - jim
		while(tokens.Any(x => x.String == "with" || x.String == "using")) {
			// "assistor" object
		}
		while(tokens.Any(x => x.String == "at" || x.String == "into" || x.String == "in" || x.String == "on")) {
			// indirect object
		}
		*/
		
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
				if (token.EntityMatches.Any(x => Player.Has(x))) {
					token.PreviousEntityMatches = token.EntityMatches;
					token.EntityMatches = token.EntityMatches.Where(x => Player.Has(x)).ToList();
				}
			}
		}

		if (tokens.Any(x => x.EntityMatches.Count > 1 && !x.Plural)) {
			return new ParseResult(tokens, ParseResultType.ErrorAmbiguousToken, tokens.FindIndex(x => x.EntityMatches.Count > 1 && !x.Plural));
		}

		// TODO: Remove this
		console.Write(tokens.Select(token => String.Format(
			"<color=\"#{0}\">{1}</color>{2}",
			token.Found ? "ffff00" : "fff",
			token.String,
			token.Found ? " [" + token.EntityMatches.Select(x => String.Format(
				"<color=\"#{0}\">{1}{2}</color>",
				token.Genitive ? "00ffff" : "ff8800",
				x,
				x.Identity == null ? String.Empty : "(" + x.Identity.Name + ")")).Flatten() + "]" : String.Empty)).Flatten(" "));

		// TODO: Actually do the thing

		return new ParseResult(tokens, ParseResultType.Success, -1);
	}
}