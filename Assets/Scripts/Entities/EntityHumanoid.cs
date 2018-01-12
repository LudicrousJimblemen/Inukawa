public class EntityHumanoid : EntityObject {
	public override void Initialize() {
		base.Initialize();
		this.Cases = new Cases {
			WordCount = 1,
			NominativeSingular = "humanoid",
			GenitiveSingular = "humanoid's",
			NominativePlural = "humanoids",
			GenitivePlural = "humanoids'"
		};

		this.AddPart(World.AddEntity<EntityLeg>());
		this.AddPart(World.AddEntity<EntityLeg>());
		this.AddPart(World.AddEntity<EntityArm>());
		this.AddPart(World.AddEntity<EntityArm>());
		this.AddPart(World.AddEntity<EntityHead>());
	}
}