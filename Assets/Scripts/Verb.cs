using System;

public class Verb {
	private string[] forms;
	public Func<Entity,VerbResult> Action { get; private set; }
    public Verb(Func<Entity, VerbResult> action, params string[] forms) {
		this.forms = new string[forms.Length];
		for (int i = 0; i < forms.Length; i++) {
			this.forms[i] = forms[i].ToLower();
		}
		Action = action;
	}

	public bool Match(string check) {
		string lowerCheck = check.ToLower();
		for (int i = 0; i < forms.Length; i++) {
			if(forms[i] == lowerCheck)
				return true;
		}
		return false;
	}

	public VerbResult Invoke (Entity indirectObject) {
		return Action.Invoke(indirectObject);
	}
}
public struct VerbResult {
	public VerbResultType ResultType { get; private set; }
	public VerbResult(VerbResultType result) {
		ResultType = result;
	}
}

public enum VerbResultType {
	Success,
	Failure
}