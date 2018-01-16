public struct Cases {
	public int WordCount;

	public string NominativeSingular;
	public string GenitiveSingular;
	public string NominativePlural;
	public string GenitivePlural;

	public string[] All {
		get {
			return new string[] {
				this.NominativeSingular,
				this.GenitiveSingular,
				this.NominativePlural,
				this.GenitivePlural
			};
		}
	}
}
