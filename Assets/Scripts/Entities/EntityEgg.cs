public class EntityEgg : EntityObject, IEntityEdible {
	public override void Initialize() {
		base.Initialize();
		this.Cases = new Cases {
			NominativeSingular = "egg",
			GenitiveSingular = "egg's",
			NominativePlural = "eggs",
			GenitivePlural = "eggs'"
		};
	}
}