public class EntityBaseballBat : EntityObject {
	public override void Initialize() {
		base.Initialize();
		this.Cases = new Cases {
			NominativeSingular = "baseball bat",
			GenitiveSingular = "baseball bat's",
			NominativePlural = "baseball bats",
			GenitivePlural = "baseball bats'"
		};
	}
}