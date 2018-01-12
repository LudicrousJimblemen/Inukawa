public class EntityHead : EntityObject {
	public override void Initialize() {
		base.Initialize();
		this.Cases = new Cases {
			WordCount = 1,
			NominativeSingular = "head",
			GenitiveSingular = "head's",
			NominativePlural = "heads",
			GenitivePlural = "heads'"
		};

		this.AddPart(World.AddEntity<EntityFace>());
	}
}