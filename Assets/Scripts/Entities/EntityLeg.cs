﻿public class EntityLeg : EntityObject {
	public override void Initialize() {
		base.Initialize();
		this.Cases = new Cases {
			WordCount = 1,
			NominativeSingular = "leg",
			GenitiveSingular = "leg's",
			NominativePlural = "legs",
			GenitivePlural = "legs'"
		};

		this.AddPart(World.AddEntity<EntityKnee>());
	}
}