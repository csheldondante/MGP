using System;
using System.Collections.Generic;

public static class CollectionUtils
{
	/// <summary>
	/// This method returns the only element in an array of reference types, null if there are 0 or more elements
	/// </summary>
	public static T Only<T>(T[] array)
	{
		System.Diagnostics.Debug.Assert (default(T) == null, "Only should never be called for arrays of value type");
		if (array.Length != 1)
			return default(T);
		return array [0];
	}

	public static List<T> SingleElementList<T>(T element){
		List<T> list = new List<T> (1);
		list.Add (element);
		return list;

	}
}

