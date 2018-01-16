public class EntitySandwich : EntityObject, IEntityEdible {
	public override void Initialize() {
		base.Initialize();
		this.Cases = new Cases {
			WordCount = 1,
			NominativeSingular = "sandwich",
			GenitiveSingular = "sandwich's",
			NominativePlural = "sandwiches",
			GenitivePlural = "sandwiches'"
		};
	}
}