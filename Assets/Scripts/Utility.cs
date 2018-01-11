using System;
using System.Collections.Generic;
using System.Linq;

public static class Utility {
	public static IEnumerable<T> Range<T>(this IEnumerable<T> enumerable, int from, int to) {
		return enumerable.Skip(from).Take(to - from);
	}

	public static string Flatten(this IEnumerable<string> enumerable, string separator) {
		return String.Join(separator, enumerable.ToArray());
	}

	public static string Flatten(this IEnumerable<string> enumerable) {
		return enumerable.Flatten(" ");
	}
}