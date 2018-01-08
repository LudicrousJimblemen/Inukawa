public class EntityHuman : EntityHumanoid {
	public virtual bool Take(Entity subject, Entity direct = null) {
		if (direct == null) {
			return false;
		} else {
			if (direct.PartOf == null) {
				subject.AddPossession(direct);
				return true;
			} else {
				return false;
			}
		}
	}

	public virtual bool Give(Entity subject, Entity direct, Entity indirect) {
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

	public virtual bool Open(IEntityOpenable subject) {
		if (subject.Open) {
			return false;
		} else {
			subject.Open = false;
			return true;
		}
	}

	public virtual bool Unlock(IEntityLockable subject, Entity key) {
		if (subject.Locked) {
			return false;
		} else {
			if (subject.Key == key) {
				subject.Locked = false;
				return true;
			} else {
				return false;
			}
		}
	}
}