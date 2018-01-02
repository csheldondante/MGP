using System;

public class Position
{
	public enum PositionType{POS2D, POS3D, UNKNOWN};

	public Position2D AsPosition2D(){
		if(this is Position2D)
			return (Position2D) this;
		return null;
		
	}

	public Position3D AsPosition3D(){
		if(this is Position3D)
			return (Position3D) this;
		return null;
	}

	public ILayered AsILayered(){
		if(GetType()!=typeof(ILayered))
			return null;
		return (ILayered) this;
	}

	public static PositionType GetPositionType(Position position){
		if (position.GetType () == typeof(Position3D))
			return PositionType.POS3D;
		if (position.GetType () == typeof(Position2D))
			return PositionType.POS2D;
		return PositionType.UNKNOWN;
	}
}

public enum LayerTag{FLOOR, CONTENT, CEILING};

public interface ILayered
{
	LayerTag GetLayer();
	void SetLayer (LayerTag tag);
}

public class Position2D : Position
{
	protected int _x;
	protected int _y;
	public Position2D (int x, int y)
	{
		_x = x;
		_y = y;
	}

	public int GetX(){
		return _x;
	}

	public int GetY(){
		return _y;
	}

	public override string ToString(){
		return "(" + _x + "," + _y + ")";
	}
}

public class Position3D : Position2D
{
	private int _z;
	public Position3D (int x, int y, int z) : base(x, y)
	{
		_z = z;
	}

	public int GetZ(){
		return _z;
	}

	public override string ToString(){
		return "(" + _x + "," + _y + "," + _z + ")";
	}
}


