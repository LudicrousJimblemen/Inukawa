public class EntityArm : EntityObject {
	public override void Initialize() {
		base.Initialize();
		this.Cases = new Cases {
			WordCount = 1,
			NominativeSingular = "arm",
			GenitiveSingular = "arm's",
			NominativePlural = "arms",
			GenitivePlural = "arms'"
		};

		this.AddPart(World.AddEntity<EntityElbow>());
	}
}