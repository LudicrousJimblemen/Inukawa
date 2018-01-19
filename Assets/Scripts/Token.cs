using System.Collections.Generic;

public class Token {
	public string String;
	public bool Found;
	public List<Entity> PreviousEntityMatches = new List<Entity>();
	public List<Entity> EntityMatches = new List<Entity>();
	public bool Genitive;
	public bool Plural;

	public bool IsVerb;
	public Verb Verb;
}