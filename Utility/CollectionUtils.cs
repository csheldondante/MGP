using System;
using UnityEngine;
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

	/// <summary>
	/// This method returns the only element in an array of reference types, null if there are 0 or more elements
	/// </summary>
	public static T Only<T>(List<T> list)
	{
		System.Diagnostics.Debug.Assert (default(T) == null, "Only should never be called for arrays of value type");
		if (list.Count != 1)
			return default(T);
		return list[0];
	}

	public static List<T> GetAsCollectionOfSubType<T, O>(List<O> list) where O : T{
		List<T> copy = new List<T> (list.Count);
		foreach(O element in list){
			copy.Add ((T)element);
		}
		return copy;
	}

	public static List<T> GetAsCollectionOfType<T>(List<object> list){
		List<T> copy = new List<T> (list.Count);
		foreach(object element in list){
			if (element is T)
				copy.Add ((T)element);
			else
				return null;
		}
		return copy;
	}


	public static List<T> SingleElementList<T>(T element){
		List<T> list = new List<T> (1);
		list.Add (element);
		return list;

	}


	/// <summary>
	/// UNSAFE!!!! :P
	/// </summary>
	public static T GetRandomEnum<T>() {
		var v = Enum.GetValues (typeof(T));
		return (T)v.GetValue (UnityEngine.Random.Range (0, v.Length));
	}

	public static T GetRandomElement<T>(T[] array) {
		return array[UnityEngine.Random.Range (0, array.Length)];
	}
}

