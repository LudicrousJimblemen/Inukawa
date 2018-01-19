using System;
using System.Collections.Generic;

public class VerbHandler {
	private static Verb Open = new Verb((n, i) => {
		VerbResultType resultType = VerbResultType.Failure;
		if (n is IEntityOpenable) {
			InputHandler.Player.Open(n as IEntityOpenable);
			resultType = VerbResultType.Success;
        }
		return new VerbResult(resultType);
	}, "open");
	private static Verb Lock = new Verb((n, i) => {
		VerbResultType resultType = VerbResultType.Failure;
		if(n is IEntityLockable) {
			InputHandler.Player.Unlock(n as IEntityLockable);
			resultType = VerbResultType.Success;
		}
		return new VerbResult(resultType);
	}, "unlock");

	// TODO implement these
	private static Verb Give;
	private static Verb Take;
	private static Verb Eat;


	public static Dictionary<string,Verb> Verbs { get; private set; } // contains multiple references to each verb, with their forms as the key
	public static void addVerb(Verb verb) {
		foreach(string form in verb.forms) {
			if(Verbs.ContainsKey(form)) {
				// oh shit collision time, TODO death
				throw new DivideByZeroException("verb collision with \"" + form + "\", you fricked up my dude. go figure it out.");
			} else {
				Verbs.Add(form,verb);
			}
		}
	}
}

public struct Verb {
	public string[] forms { get; private set; }
	private Func<Entity,Entity,VerbResult> Action;
    public Verb(Func<Entity, Entity, VerbResult> action, params string[] forms) {
		this.forms = new string[forms.Length];
		for (int i = 0; i < forms.Length; i++) {
			this.forms[i] = forms[i].ToLower();
		}
		Action = action;
		VerbHandler.addVerb(this);
	}

	public bool Match(string check) {
		string lowerCheck = check.ToLower();
		for (int i = 0; i < forms.Length; i++) {
			if(forms[i] == lowerCheck)
				return true;
		}
		return false;
	}

	public VerbResult Invoke (Entity nominative, Entity indirectObject) {
		return Action.Invoke(nominative, indirectObject);
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