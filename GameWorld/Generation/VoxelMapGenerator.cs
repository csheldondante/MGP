using System;
using UnityEngine;


public static class VoxelMapGenerator{
	
	public static MultiLevelVoxelMap2D GenerateMap(int sizeX, int sizeY, int numLayers, int seed){
		MultiLevelVoxelMap2D map = new MultiLevelVoxelMap2D (sizeX, sizeY, numLayers);
		for (int i = 0; i < sizeX; ++i) {
			for (int j = 0; j < sizeY; ++j) {
				GroundTile.GroundTileType type;
				if (i > 0 && UnityEngine.Random.value > .25) {
					type = map.GetGround (i-1, j).GetTileType ();
				} else if (j > 0 && UnityEngine.Random.value > .25) {
					type = map.GetGround (i, j - 1).GetTileType ();
				} else {
					type = CollectionUtils.GetRandomElement (new GroundTile.GroundTileType[] {
						GroundTile.GroundTileType.DIRT,
						GroundTile.GroundTileType.WATER_SHALLOW
					});
				}
				IPlaceable tile = new GroundTile(type);
				map.Place (tile, i, j, 0);
			}
		}
		return map;
	}

	public static void Generate2DTileMap<T>(T[,] emptyMap, T[] types){
		for (int i = 0; i < emptyMap.GetLength (0); ++i) {
			for (int j = 0; j < emptyMap.GetLength (1); ++j) {
				if (i > 0 && UnityEngine.Random.value > .25) {
					emptyMap[i,j] = emptyMap[i-1,j];
				} else if (j > 0 && UnityEngine.Random.value > .25) {
					emptyMap[i,j] = emptyMap[i,j-1];
				} else {
					emptyMap[i,j] = CollectionUtils.GetRandomElement (types);
				}				
			}
		}
	}

}
