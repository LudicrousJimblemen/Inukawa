public enum ParseResultType {
	Success
}

public struct ParseResult {
	public ParseResultType ResultType;

	public ParseResult(ParseResultType resultType) {
		this.ResultType = resultType;
	}
}