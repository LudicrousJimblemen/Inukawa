public class EntityDog : EntityObject {
	public override void Initialize() {
		base.Initialize();
		this.Cases = new Cases {
			WordCount = 1,
			NominativeSingular = "dog",
			GenitiveSingular = "dog's",
			NominativePlural = "dogs",
			GenitivePlural = "dogs'"
		};
	}
}