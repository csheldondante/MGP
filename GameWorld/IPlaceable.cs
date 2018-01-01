using System;
using System.Collections.Generic;

public interface IPlaceable
{
	bool CanPlace(List<Position> positions, IGameMap map);
	bool CanColocate (IPlaceable placeable);
	bool Place(List<Position> positions);
}
