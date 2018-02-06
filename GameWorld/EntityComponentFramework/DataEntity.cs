using System;
using System.Collections;
using System.Collections.Generic;

public class DataEntity{
	List<IDataComponent> _dataComponents = new List<IDataComponent>();
	public List<IAction> _inProgressActions = new List<IAction>();

	public List<T> GetComponents<T>() where T : IDataComponent{
		List<T> results = new List<T> ();
		foreach (IDataComponent component in _dataComponents) {
			if(component is T){
				results.Add((T)component);
			}
		}
		return results;
	}

	public T GetComponent<T>() where T : IDataComponent{
		foreach (IDataComponent component in _dataComponents) {
			if(component is T){
				return (T)component;
			}
		}
		return default(T);
	}

	/// <summary>
	/// This should only ever be called by DataComponent base constructor
	/// </summary>
	/// <param name="component">Component.</param>
	public void AddDataComponent(IDataComponent component){
		_dataComponents.Add (component);
	}

	public void UpdateActions(double deltaTime){
		foreach (IAction action in _inProgressActions) {
			action.Update (deltaTime);
		}
	}

	public bool IsIdle(){
		return 0 == _inProgressActions.Count;
	}
}
