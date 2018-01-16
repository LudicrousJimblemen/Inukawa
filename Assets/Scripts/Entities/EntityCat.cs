public class EntityCat : EntityObject {
	public override void Initialize() {
		base.Initialize();
		this.Cases = new Cases {
			WordCount = 1,
			NominativeSingular = "cat",
			GenitiveSingular = "cat's",
			NominativePlural = "cats",
			GenitivePlural = "cats'"
		};
	}
}