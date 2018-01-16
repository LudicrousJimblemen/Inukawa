using System;

public class WorldAction {
	public string Id;
	
	public Func<Entity, Entity, Entity, bool> Function;
}