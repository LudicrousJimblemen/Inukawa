public class EntityBox : EntityObject, IEntityOpenable, IEntityContainer {
	public override void Initialize() {
		base.Initialize();
		this.Cases = new Cases {
			NominativeSingular = "chest",
			GenitiveSingular = "chest's",
			NominativePlural = "chests",
			GenitivePlural = "chests'"
		};
	}

	public bool Open { get; set; }
}