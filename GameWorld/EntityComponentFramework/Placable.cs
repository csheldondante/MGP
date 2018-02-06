using System;

public class Placeable : DataComponent
{
	private Position _position;

	public Placeable(DataEntity entity) : base(entity){
		_position = new Position2D (UnityEngine.Random.Range (0, 10), UnityEngine.Random.Range (0, 10));
	}

	public Position getPosition(){
		return _position;
	}
}
