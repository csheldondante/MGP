using System;
using System.Collections.Generic;

public interface IGameMap
{

	List<List<IPlaceable>> GetContent (List<Position> positions);
	List<IPlaceable> GetContent (Position position);
	bool Place(IPlaceable placeable, List<Position> position);
	bool Remove(IPlaceable placeable);
	bool Move(IPlaceable placeable, List<Position> position);
	bool Generate();
}