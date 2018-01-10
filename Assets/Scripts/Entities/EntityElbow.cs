public class EntityElbow : EntityObject {
	public override void Initialize() {
		base.Initialize();
		this.Cases = new Cases {
			NominativeSingular = "elbow",
			GenitiveSingular = "elbow's",
			NominativePlural = "elbows",
			GenitivePlural = "elbows'"
		};
	}
}