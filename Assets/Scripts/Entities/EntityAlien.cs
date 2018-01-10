public class EntityAlien : EntityHumanoid {
	public override void Initialize() {
		base.Initialize();
		this.Cases = new Cases {
			NominativeSingular = "alien",
			GenitiveSingular = "alien's",
			NominativePlural = "aliens",
			GenitivePlural = "aliens'"
		};
	}
}