using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TestScript : MonoBehaviour {
	public Text Text;

	private void Start() {
		StartCoroutine(Test());
	}

	private IEnumerator Test() {
		//
		yield return new WaitForSeconds(0.2f);
	}

	private void Write(string text) {
		Text.text += "\n\n" + text;
	}

	private void OnDrawGizmos() {
		const int square = 11;
		float half = (square - 1) / 2f;

		Gizmos.color = Color.white;
		for (int i = 0; i < World.Entities.Count; i++) {
			Gizmos.DrawSphere(new Vector3(i % square - half, i / square - half), 0.3f);
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
