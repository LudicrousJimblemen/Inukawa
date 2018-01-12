public class EntityFace : EntityObject {
	public override void Initialize() {
		base.Initialize();
		this.Cases = new Cases {
			WordCount = 1,
			NominativeSingular = "face",
			GenitiveSingular = "face's",
			NominativePlural = "faces",
			GenitivePlural = "faces'"
		};
	}
}