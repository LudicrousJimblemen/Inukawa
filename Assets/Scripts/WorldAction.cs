using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// An action performable by a <see cref="Entity"/>.
/// </summary>
public class WorldAction {
	/// <summary>
	/// A unique identifier for this <see cref="WorldAction"/>.
	/// </summary>
	public string Id;

	/// <summary>
	/// The function to execute on the subject, direct object, and indirect object, returning true if the action is successful.
	/// </summary>
	public Func<Entity, Entity, Entity, bool> Function;
}