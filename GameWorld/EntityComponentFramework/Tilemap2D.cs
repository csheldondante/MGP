using System;
using System.Collections;
using System.Collections.Generic;

public class Tilemap2D : Viewable {
	public enum TileType{DIRT, WATER};
	public readonly TileType[,] tiles;

	public Tilemap2D(DataEntity entity) : this(256, 256, entity){
	}

	public Tilemap2D(int sizeX, int sizeY, DataEntity entity) : base (entity) {
		tiles= new TileType[sizeX,sizeY];
		VoxelMapGenerator.Generate2DTileMap (tiles, (TileType[])Enum.GetValues(typeof(TileType)));
	}
}
