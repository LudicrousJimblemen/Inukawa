using System.Collections;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System;

public class TestScript : MonoBehaviour {
	public Text Text;

	private Entity player;

	private void Start() {
		player = World.AddEntity("human", new Identity("Inukawa"), Location.Get("house"));

		World.AddEntity("baby", new Identity("Bab"), Location.Get("house"));
		World.AddEntity("egg", null, Location.Get("house"));

		StartCoroutine(Test());
	}

	private IEnumerator Test() {
		Write("You in a house.");
		yield return new WaitForSeconds(0.5f);
		Act("take", World.GetEntityByArchetype("egg"));
		yield return new WaitForSeconds(0.5f);
		Act("give", World.GetEntityByArchetype("egg"), World.GetEntityByArchetype("baby"));
		yield return new WaitForSeconds(0.5f);
		Act("kick", World.GetEntityByArchetype("baby"));
		yield return new WaitForSeconds(0.5f);
		Act("kick", World.GetEntityByArchetype("egg"));
		yield return new WaitForSeconds(0.5f);
		Act("take", World.GetEntityByArchetype("egg"));
		yield return new WaitForSeconds(0.5f);
	}

	private void Act(string action, Entity directObject = null, Entity indirectObject = null) {
		bool success = player.Act(action, directObject, indirectObject);
		// TODO: I have no idea what I am doing.

		Write(String.Format(
			"You{0} {1}{2}{3}.",
			success ? String.Empty : " are unable to",
			action,
			directObject == null ? String.Empty : directObject.Identity == null ? " the " + directObject.Archetype.Id : " " + directObject.Identity.Name,
			indirectObject == null ? String.Empty : " from/with/to " + (indirectObject.Identity == null ? " the " + indirectObject.Archetype.Id : indirectObject.Identity.Name)
		));
	}

	private void Write(string text) {
		Text.text += "\n\n" + text;
	}

	private const int square = 11;
	private float half = (square - 1) / 2f;

	private void OnDrawGizmos() {
		Gizmos.color = Color.white;
		for (int i = 0; i < World.Entities.Count; i++) {
			Gizmos.DrawSphere(new Vector3(i % square - half, i / square - half), 0.3f);
			Handles.Label(new Vector3(i % square - half - 0.2f, i / square - half - 0.2f), World.Entities[i].Archetype.Id);
		}

		for (int i = 0; i < World.Entities.Count; i++) {
			Gizmos.color = Color.red;
			foreach (var part in World.Entities[i].Parts) {
				int j = World.Entities.IndexOf(part);
				Gizmos.DrawLine(new Vector3(i % square - half + 0.01f, i / square - half), new Vector3(j % square - half, j / square - half));
			}

			Gizmos.color = Color.green;
			foreach (var possession in World.Entities[i].Possessions) {
				int j = World.Entities.IndexOf(possession);
				Gizmos.DrawLine(new Vector3(i % square - half + 0.02f, i / square - half), new Vector3(j % square - half, j / square - half));
			}

			Gizmos.color = Color.blue;
			foreach (var reference in World.Entities[i].References) {
				int j = World.Entities.IndexOf(reference.Entity);
				Gizmos.DrawLine(new Vector3(i % square - half + 0.03f, i / square - half), new Vector3(j % square - half, j / square - half));
			}
		}
	}
}
