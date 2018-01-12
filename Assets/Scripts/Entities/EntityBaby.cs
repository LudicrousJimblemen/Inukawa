public class EntityBaby : EntityHuman {
	public override void Initialize() {
		base.Initialize();
		this.Cases = new Cases {
			WordCount = 1,
			NominativeSingular = "baby",
			GenitiveSingular = "baby's",
			NominativePlural = "babies",
			GenitivePlural = "babies'"
		};
	}
}