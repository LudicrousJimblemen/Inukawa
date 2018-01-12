public class EntityKey : EntityObject {
	public override void Initialize() {
		base.Initialize();
		this.Cases = new Cases {
			WordCount = 1,
			NominativeSingular = "key",
			GenitiveSingular = "key's",
			NominativePlural = "keys",
			GenitivePlural = "keys'"
		};
	}
}