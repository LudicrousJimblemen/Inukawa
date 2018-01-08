public class EntityLeg : EntityObject {
	public override void Initialize() {
		this.AddPart(World.AddEntity<EntityKnee>());
	}
}