public class EntityObject : Entity {
	public override void Initialize() {
		this.Cases = new Cases {
			WordCount = 1,
			NominativeSingular = "object",
			GenitiveSingular = "object's",
			NominativePlural = "objects",
			GenitivePlural = "objects'"
		};
	}
}
