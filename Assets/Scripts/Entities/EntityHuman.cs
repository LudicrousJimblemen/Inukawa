public class EntityHuman : EntityHumanoid {
	public override void Initialize() {
		base.Initialize();
		this.Cases = new Cases {
			NominativeSingular = "human",
			GenitiveSingular = "human's",
			NominativePlural = "humans",
			GenitivePlural = "humans'"
		};
	}

	public virtual void Take(Entity direct) {
		this.AddPossession(direct);
	}

	public virtual void Give(Entity direct, Entity indirect) {
		indirect.AddPossession(direct);
	}

	public virtual void Open(IEntityOpenable direct) {
		direct.Open = true;
	}

	public virtual void Unlock(IEntityLockable direct) {
		direct.Locked = false;
	}

	public virtual void Eat(Entity direct) {
		World.RemoveEntity(direct);
	}
}