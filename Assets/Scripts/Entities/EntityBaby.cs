public class EntityBaby : EntityHuman {
	public override void Initialize() {
		base.Initialize();
		this.Cases = new Cases {
			NominativeSingular = "baby",
			GenitiveSingular = "baby's",
			NominativePlural = "babies",
			GenitivePlural = "babies'"
		};
	}
}