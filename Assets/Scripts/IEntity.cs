using System.Collections.Generic;

public interface IEntity{
	T GetComponent<T>() where T : IComponent;
	T GetComponent<T>(IComparer<T> comparator) where T : IComponent;
	List<T> GetComponents<T>() where T : IComponent;
	bool AddComponent<T> (T component) where T :IComponent;
}
