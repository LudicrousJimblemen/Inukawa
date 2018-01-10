public class EntityKey : EntityObject {
	public override void Initialize() {
		base.Initialize();
		this.Cases = new Cases {
			NominativeSingular = "key",
			GenitiveSingular = "key's",
			NominativePlural = "keys",
			GenitivePlural = "keys'"
		};
	}
}