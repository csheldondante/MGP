using System;
using System.Collections.Generic;

public interface IPlaceable
{
	bool CanPlace(Position position, IGameMap map);
	bool CanColocate (IPlaceable placeable);
	bool Place(Position positions);
	Position GetPosition();
}

public class VoxelPlaceable : IPlaceable {
	private Position3D _position;

	public bool CanPlace(Position position, IGameMap map){
		return true;
	}

	public bool CanColocate(IPlaceable placeable){
		return true;
	}

	public bool Place(Position position){
		_position = position.AsPosition3D ();
		return true;
	}

	public Position GetPosition(){
		return (Position) _position;
	}
}
