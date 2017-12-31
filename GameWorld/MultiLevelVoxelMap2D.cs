using System;
using System.Collections.Generic;

public class MultiLevelVoxelMap2D : IGameMap
{
	private static int _defaultMapSize=256;
	private VoxelMap2DLayer[] layers;
	public MultiLevelVoxelMap2D ()
	{
		layers = new VoxelMap2DLayer[1];
		layers [0] = new VoxelMap2DLayer (_defaultMapSize, _defaultMapSize);
	}

	public List<IPlaceable> GetContent (Position position){
		return null;
	}
	public List<List<IPlaceable>> GetContent (List<Position> positions){
		return null;
	}
	public Position GetPosition (IPlaceable placeable){
		return null;
	}
	public bool Place(IPlaceable placeable, Position position){
		return true;
	}
	public bool Remove(IPlaceable placeable){
		return true;
	}
	public bool Move(IPlaceable placeable, Position position){
		return true;
	}
	public bool Generate(){
		return true;
	}

	private class VoxelMap2DLayer : ISpatialIndex, IGameMap{
		private readonly int _sizeX;
		private readonly int _sizeY;
		private List<IPlaceable>[,] _floor;
		private List<IPlaceable>[,] _content;
		private List<IPlaceable>[,] _ceiling;

		public VoxelMap2DLayer(int sizeX, int sizeY){
			_sizeX=sizeX;
			_sizeY=sizeY;
			_floor = new List<IPlaceable>[_sizeX,_sizeY];
			_content = new List<IPlaceable>[_sizeX,_sizeY];
			_ceiling = new List<IPlaceable>[_sizeX,_sizeY];
		}

		public List<IPlaceable> GetContent (Position position){
			return null;
		}
		public List<List<IPlaceable>> GetContent (List<Position> positions){
			return null;
		}
		public Position GetPosition (IPlaceable placeable){
			return null;
		}
		public bool Place(IPlaceable placeable, Position position){
			return true;
		}
		public bool Remove(IPlaceable placeable){
			return true;
		}
		public bool Move(IPlaceable placeable, Position position){
			return true;
		}
		public bool Generate(){
			return true;
		}
		public void Index(IPlaceable obj, Position position){
		}

		public List<IPlaceable> GetContents(Position position){
			return null;
		}

	}
}


