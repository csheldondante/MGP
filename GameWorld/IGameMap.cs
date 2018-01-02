using System;
using System.Collections.Generic;

public interface IGameMap
{
	List<IPlaceable> GetContent (Position position);
	List<IPlaceable> GetContent ();
	bool Place(IPlaceable placeable, Position position);
	bool Remove(IPlaceable placeable);
}