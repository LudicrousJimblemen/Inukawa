public class EntityElbow : EntityObject {
	public override void Initialize() {
		base.Initialize();
		this.Cases = new Cases {
			WordCount = 1,
			NominativeSingular = "elbow",
			GenitiveSingular = "elbow's",
			NominativePlural = "elbows",
			GenitivePlural = "elbows'"
		};
	}
}