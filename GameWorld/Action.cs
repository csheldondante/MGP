using System;
using System.Collections;
using System.Collections.Generic;

public interface IAction{
	bool CanPerform(IActor agent);
	double Update (double deltaTime);
	bool Pause();
	bool Resume();
	void Cancel();
}

public interface IActor{
	bool CanPerform(IAction action, IInteractable target);
	List<IAction> GetAvailableActions (IInteractable target);
	IEnumerator Perform(IAction action, IInteractable target);
}