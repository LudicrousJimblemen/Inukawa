public class EntityDoor : EntityObject, IEntityOpenable, IEntityLockable {
	public override void Initialize() {
		base.Initialize();
		this.Cases = new Cases {
			NominativeSingular = "door",
			GenitiveSingular = "door's",
			NominativePlural = "doors",
			GenitivePlural = "doors'"
		};
	}

	public Entity Key { get; set; }

	public bool Open { get; set; }

	public bool Locked { get; set; }
}