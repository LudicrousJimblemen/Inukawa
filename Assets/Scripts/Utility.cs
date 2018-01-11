using System;
using System.Collections.Generic;
using System.Linq;

public static class Utility {
	public static IEnumerable<T> Range<T>(this IEnumerable<T> enumerable, int from, int to) {
		return enumerable.Skip(from).Take(to - from);
	}

	public static string Flatten(this IEnumerable<string> enumerable, string separator) {
		UnityEngine.Debug.Log(enumerable);
		return String.Join(separator, enumerable.ToArray());
	}

	public static string Flatten(this IEnumerable<string> enumerable) {
		return enumerable.Flatten(" ");
	}

	public static string Flatten<T>(this IEnumerable<T> enumerable, string separator) {
		return enumerable.Select(x => x.ToString()).Flatten(separator);
	}

	public static string Flatten<T>(this IEnumerable<T> enumerable) {
		return enumerable.Flatten(" ");
	}
}