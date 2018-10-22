using System.Collections.Generic;

public class Unit : IEntity{
	public List<IComponent> components = new List<IComponent> ();

	public List<T> GetComponents<T>() where T : IComponent{
		List<T> components = new List<T> ();
		foreach (IComponent component in components) {
			if (component.GetType () == typeof(T))
				components.Add ((T)component);
		}
		return components;
	}

	public T GetComponent<T>() where T : IComponent{
		foreach (IComponent component in components) {
			if (component.GetType () == typeof(T))
				return ((T)component);
		}
		return default(T);
	}

	public T GetComponent<T>(IComparer<T> comparator) where T : IComponent{;
		List<T> componentsOfType = GetComponents<T> ();
		componentsOfType.Sort (comparator);
		return componentsOfType.Count > 0 ? componentsOfType[0] : default(T);
	}

	public bool AddComponent<T> (T component) where T : IComponent{
		if (component.GetEntity () != null)
			return false;
		component.SetEntity (this);
		components.Add (component);
		return true;
	}
}
