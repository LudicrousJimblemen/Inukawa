public class EntityHuman : EntityHumanoid {
	public virtual bool Take(Entity direct = null, Entity indirect = null) {
		if (direct == null) {
			return false;
		} else {
			if (direct.Accessible()) {
				this.AddPossession(direct);
				return true;
			} else {
				return false;
			}
		}
	}

	public virtual bool Give(Entity direct, Entity indirect) {
		if (direct == null) {
			return false;
		} else {
			if (direct.PartOf == null) {
				if (indirect == null) {
					return false;
				} else {
					indirect.AddPossession(direct);
					return true;
				}
			} else {
				return false;
			}
		}
	}

	public virtual bool Open(IEntityOpenable direct) {
		if (direct.Open) {
			return false;
		} else {
			IEntityLockable lockable = direct as IEntityLockable;
			if (direct != null) {
				if (lockable.Locked) {
					return false;
				} else {
					direct.Open = true;
					return true;
				}
			} else {
				direct.Open = true;
				return true;
			}
		}
	}

	public virtual bool Unlock(IEntityLockable direct, Entity key) {
		if (!direct.Locked) {
			return false;
		} else {
			if (direct.Key == key) {
				direct.Locked = false;
				return true;
			} else {
				return false;
			}
		}
	}

	public virtual bool Eat(Entity direct) {
		if (direct is IEntityEdible) {
			World.RemoveEntity(direct);
			return true;
		} else {
			return false;
		}
	}
}