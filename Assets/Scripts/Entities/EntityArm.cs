public class EntityArm : EntityObject {
	public override void Initialize() {
		this.AddPart(World.AddEntity<EntityElbow>());
	}
}