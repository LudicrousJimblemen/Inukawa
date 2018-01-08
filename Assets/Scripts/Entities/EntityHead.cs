public class EntityHead : EntityObject {
	public override void Initialize() {
		this.AddPart(World.AddEntity<EntityFace>());
	}
}