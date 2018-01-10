public class EntityObject : Entity {
	public override void Initialize() {
		this.Cases = new Cases {
			NominativeSingular = "object",
			GenitiveSingular = "object's",
			NominativePlural = "objects",
			GenitivePlural = "objects'"
		};
	}
}
