public class EntityEgg : EntityObject, IEntityEdible {
	public override void Initialize() {
		base.Initialize();
		this.Cases = new Cases {
			WordCount = 1,
			NominativeSingular = "egg",
			GenitiveSingular = "egg's",
			NominativePlural = "eggs",
			GenitivePlural = "eggs'"
		};
	}
}