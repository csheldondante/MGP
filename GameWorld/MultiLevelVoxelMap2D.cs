using System;
using System.Collections.Generic;

public class MultiLevelVoxelMap2D : IGameMap
{
	private readonly int _sizeX=256;
	private readonly int _sizeY=256;
	private readonly int _sizeZ=1;
	private VoxelMap2DLayer[] _layers;
	public MultiLevelVoxelMap2D ()
	{
		_layers = new VoxelMap2DLayer[_sizeZ];
		for (int i = 0; i < _sizeZ; ++i) {
			_layers [i] = new VoxelMap2DLayer (_sizeX, _sizeY);
		}
	}

	public bool Place(IPlaceable placeable, List<Position> positions){
		List<Position3D> positions3D = GetValid3DPositions (positions);

		if (!CanPlace (placeable, positions, positions3D))
			return false;
		bool addedSuccessfully = true;
		foreach (Position3D pos3D in positions3D) {
			VoxelMap2DLayer layer = _layers [pos3D.GetZ()];
			if(layer==null)
				layer = new VoxelMap2DLayer(_sizeX, _sizeY);
			addedSuccessfully=addedSuccessfully&&layer.Place (placeable, CollectionUtils.SingleElementList((Position)pos3D));//TODO this is sligthly inefficient because the check happens at both the 3D map and the 2D slice...
		}
		System.Diagnostics.Debug.Assert (addedSuccessfully, "3D map and 2D slice should match in terms of whether or not this can be added");
		return addedSuccessfully;
	}

	public bool CanPlace(IPlaceable placeable, List<Position> positions){
		List<Position3D> positions3D = GetValid3DPositions (positions);
		if (!CanPlace (placeable, positions, positions3D))
			return false;
		return true;
	}

	private bool CanPlace(IPlaceable placeable, List<Position> positions, List<Position3D> positions3D){
		if (positions3D == null)
			return false;

		if (!placeable.CanPlace (positions, this))
			return false;

		List<List<IPlaceable>> overlappingContent = GetContent (positions);
		if (overlappingContent == null)
			return false;

		foreach(List<IPlaceable> currentContent in overlappingContent){
			foreach (IPlaceable currentOccupant in currentContent) {
				if (!placeable.CanColocate (placeable))
					return false;
			}
		}

		return true;
	}

	public List<List<IPlaceable>> GetContent (List<Position> positions){
		List<List<IPlaceable>> contents = new List<List<IPlaceable>> ();
		foreach (Position pos in positions) {
			List<IPlaceable> content = GetContent (pos);
			if (content == null)
				return null;
			contents.Add(content);
		}
		return contents;
	}

	//TODO maybe replace this with an arbitrary query instead of searching just by position
	//for example we might search for a list of all sources of danger within some distance
	public List<IPlaceable> GetContent (Position position){
		Position3D position3D = GetPosition3D (position);
		if (position3D == null)
			return null;
		VoxelMap2DLayer layer = _layers[position3D.GetZ()];
		return layer == null ? new List<IPlaceable> () : layer.GetContent(position);
	}

	public Position GetPosition (IPlaceable placeable){
		return null;
	}

	public bool Remove(IPlaceable placeable){
		return true;
	}
	public bool Move(IPlaceable placeable, List<Position> position){
		return true;
	}
	public bool Generate(){
		return true;
	}

	private List<Position3D> GetValid3DPositions(List<Position> positions){
		List<Position3D> validPositions = new List<Position3D> ();
		foreach (Position pos in positions) {
			Position3D pos3D = GetPosition3D (pos);
			if (pos3D == null)
				return null;
			validPositions.Add (pos3D);
		}
		return validPositions;
	}

	/// <summary>
	/// Returns null if the position is not valid
	/// </summary>
	private Position3D GetPosition3D(Position position){
		Position3D position3D = position.AsPosition3D ();
		if (position3D == null)
			return null;
		int x = position3D.GetX ();
		int y = position3D.GetY ();
		int z = position3D.GetZ ();
		if (x >= _sizeX || y >= _sizeY || z >= _sizeZ)
			return null;
		return position3D;
	}

	private class VoxelMap2DLayer : ISpatialIndex, IGameMap{
		private readonly int _sizeX;
		private readonly int _sizeY;
		private List<IPlaceable>[,] _content;

		public VoxelMap2DLayer(int sizeX, int sizeY){
			_sizeX=sizeX;
			_sizeY=sizeY;
			_content = new List<IPlaceable>[_sizeX,_sizeY];
		}

		public bool Place(IPlaceable placeable, List<Position> positions){
			List<Position2D> positions2D = GetValid2DPositions (positions);

			if (!CanPlace (placeable, positions, positions2D))
				return false;

			foreach (Position2D pos2D in positions2D) {
				List<IPlaceable> contents = _content [pos2D.GetX (), pos2D.GetY ()];
				if(contents==null)
					contents = new List<IPlaceable>();
				contents.Add (placeable);
			}
			return placeable.Place (positions);
		}

		public bool CanPlace(IPlaceable placeable, List<Position> positions){
			List<Position2D> positions2D = GetValid2DPositions (positions);
			if (!CanPlace (placeable, positions, positions2D))
				return false;
			return true;
		}

		private bool CanPlace(IPlaceable placeable, List<Position> positions, List<Position2D> positions2D){
			if (positions2D == null)
				return false;
			
			if (!placeable.CanPlace (positions, this))
				return false;

			List<List<IPlaceable>> overlappingContent = GetContent (positions);
			if (overlappingContent == null)
				return false;

			foreach(List<IPlaceable> currentContent in overlappingContent){
				foreach (IPlaceable currentOccupant in currentContent) {
					if (!placeable.CanColocate (placeable))
						return false;
				}
			}

			return true;
		}

		public List<List<IPlaceable>> GetContent (List<Position> positions){
			List<List<IPlaceable>> contents = new List<List<IPlaceable>> ();
			foreach (Position pos in positions) {
				List<IPlaceable> content = GetContent (pos);
				if (content == null)
					return null;
				contents.Add(content);
			}
			return contents;
		}

		//TODO maybe replace this with an arbitrary query instead of searching just by position
		//for example we might search for a list of all sources of danger within some distance
		public List<IPlaceable> GetContent (Position position){
			Position2D position2D = GetPosition2D (position);
			if (position2D == null)
				return null;
			ILayered layer = position.AsILayered ();
			if (layer == null)
				return null;
			List<IPlaceable> content = _content [position2D.GetX(),position2D.GetY()];
			return content == null ? new List<IPlaceable> () : content;
		}


		public bool Remove(IPlaceable placeable){
			return true;
		}
		public bool Move(IPlaceable placeable, List<Position> positions){
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

		private List<Position2D> GetValid2DPositions(List<Position> positions){
			List<Position2D> validPositions = new List<Position2D> ();
			foreach (Position pos in positions) {
				Position2D pos2D = GetPosition2D (pos);
				if (pos2D == null)
					return null;
				validPositions.Add (pos2D);
			}
			return validPositions;
		}

		/// <summary>
		/// Returns null if the position is not valid
		/// </summary>
		private Position2D GetPosition2D(Position position){
			Position2D position2D = position.AsPosition2D ();
			if (position2D == null)
				return null;
			int x = position2D.GetX ();
			int y = position2D.GetY ();
			if (x >= _sizeX || y >= _sizeY)
				return null;
			return position2D;
		}
	}
}


