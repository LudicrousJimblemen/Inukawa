using System.Collections.Generic;

public enum ParseResultType {
	Success,
	ErrorUnpairedGenitive,
	ErrorInvalidGenitive,
	ErrorAmbiguousToken
}

public struct ParseResult {
	public List<Token> Tokens;
	public ParseResultType ResultType;
	public int Index;

	public ParseResult(List<Token> tokens, ParseResultType resultType, int index) {
		this.Tokens = tokens;
		this.ResultType = resultType;
		this.Index = index;
	}
}