using System;

public class GroundTile : VoxelPlaceable
{
	public enum GroundTileType{DIRT, AIR, WATER_SHALLOW, WATER_DEEP, GRASS, LAVA};
	private GroundTileType _type;

	public GroundTile (GroundTileType type)
	{
		_type = type;
	}

	public GroundTileType GetTileType(){
		return _type;
	}
}


