public class EntityKnee : EntityObject {
	public override void Initialize() {
		base.Initialize();
		this.Cases = new Cases {
			WordCount = 1,
			NominativeSingular = "knee",
			GenitiveSingular = "knee's",
			NominativePlural = "knees",
			GenitivePlural = "knees'"
		};
	}
}