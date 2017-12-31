using System;

public abstract class Position
{
}

public class Position2D : Position
{
	public int _x;
	public int _y;
	public Position2D (int x, int y)
	{
		_x = x;
		_y = y;
	}
}
