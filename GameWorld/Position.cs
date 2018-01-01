using System;

public class Position
{
	public Position2D AsPosition2D(){
		if(GetType()!=typeof(Position2D))
			return null;
		return (Position2D) this;
	}

	public Position3D AsPosition3D(){
		if(GetType()!=typeof(Position3D))
			return null;
		return (Position3D) this;
	}

	public ILayered AsILayered(){
		if(GetType()!=typeof(ILayered))
			return null;
		return (ILayered) this;
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
	private int _x;
	private int _y;
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
}

public class LayeredPos2D : Position2D, ILayered{
	protected LayerTag _tag;
	public LayeredPos2D (int x, int y, LayerTag tag) : base(x, y){
		SetLayer (tag);
	}

	public LayerTag GetLayer(){
		return _tag;
	}

	public void SetLayer(LayerTag tag){
		_tag = tag;
	}
}

public class LayeredPos3D : Position3D, ILayered{
	protected LayerTag _tag;
	public LayeredPos3D (int x, int y, int z, LayerTag tag) : base(x, y , z){
		SetLayer (tag);
	}

	public LayerTag GetLayer(){
		return _tag;
	}

	public void SetLayer(LayerTag tag){
		_tag = tag;
	}
}


