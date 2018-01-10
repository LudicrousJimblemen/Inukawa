public class EntityKnee : EntityObject {
	public override void Initialize() {
		base.Initialize();
		this.Cases = new Cases {
			NominativeSingular = "knee",
			GenitiveSingular = "knee's",
			NominativePlural = "knees",
			GenitivePlural = "knees'"
		};
	}
}