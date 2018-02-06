using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public interface IActionSource{
	List<IAction> GetProvidedActions (IActor actor, IInteractable target);
}

public interface IInteractable : IActionSource{
	IEnumerator PerformAction(IAction action);
}
