public class EntityArm : EntityObject {
	public override void Initialize() {
		base.Initialize();
		this.Cases = new Cases {
			NominativeSingular = "arm",
			GenitiveSingular = "arm's",
			NominativePlural = "arms",
			GenitivePlural = "arms'"
		};

		this.AddPart(World.AddEntity<EntityElbow>());
	}
}