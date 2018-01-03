using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class WorldAction {
	public string Name;
	public Func<WorldObject, WorldObject, WorldObject, bool> Function;
}