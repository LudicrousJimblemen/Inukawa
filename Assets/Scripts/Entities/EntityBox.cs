public class EntityBox : EntityObject, IEntityOpenable, IEntityContainer {
	public override void Initialize() {
		base.Initialize();
		this.Cases = new Cases {
			WordCount = 1,
			NominativeSingular = "box",
			GenitiveSingular = "box's",
			NominativePlural = "boxes",
			GenitivePlural = "boxes'"
		};
	}

	public bool Open { get; set; }
}