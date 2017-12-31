using System;
using System.Collections.Generic;

public interface IGameMap
{
	List<IPlaceable> GetContent (Position position);
	List<List<IPlaceable>> GetContent (List<Position> positions);
	Position GetPosition (IPlaceable placeable);
	bool Place(IPlaceable placeable, Position position);
	bool Remove(IPlaceable placeable);
	bool Move(IPlaceable placeable, Position position);
	bool Generate();
}