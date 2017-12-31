using System;

public interface IPlaceable
{
	bool IsValid(Position position, IGameMap map);
	bool Place(Position position);
}
