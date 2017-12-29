using System;

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
}

