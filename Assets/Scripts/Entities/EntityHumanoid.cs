public class EntityHumanoid : EntityObject {
	public override void Initialize() {
		this.AddPart(World.AddEntity<EntityLeg>());
		this.AddPart(World.AddEntity<EntityLeg>());
		this.AddPart(World.AddEntity<EntityArm>());
		this.AddPart(World.AddEntity<EntityArm>());
		this.AddPart(World.AddEntity<EntityHead>());
	}
}