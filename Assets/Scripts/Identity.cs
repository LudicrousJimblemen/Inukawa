/// <summary>
/// Defines specific information about a single <see cref="Entity"/>.
/// </summary>
public class Identity {
	/// <summary>
	/// The name of the <see cref="Entity"/>.
	/// </summary>
	public string Name;

	/// <summary>
	/// Creates an identity.
	/// </summary>
	/// <param name="name">The name of the <see cref="Entity"/></param>
	public Identity(string name) {
		this.Name = name;
	}
}