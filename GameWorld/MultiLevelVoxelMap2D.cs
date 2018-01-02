using System;
using UnityEngine;
using System.Collections.Generic;

public class MultiLevelVoxelMap2D : IGameMap
{
	private readonly int _sizeX;
	private readonly int _sizeY;
	private readonly int _sizeZ;
	private readonly VoxelMap2DLayer[] _layers;

	public MultiLevelVoxelMap2D (int sizeX, int sizeY, int numLayers)
	{
		_sizeX=sizeX;
		_sizeY=sizeY;
		_sizeZ = numLayers;
		_layers = new VoxelMap2DLayer[_sizeZ];
		for (int i = 0; i < _sizeZ; ++i) {
			_layers [i] = new VoxelMap2DLayer (_sizeX, _sizeY);
		}
	}

	/// <summary>
	/// This returns the first ground tile in the unordered list of contents at the specified (x,y) in the highest z coordinate where one is present
	/// </summary>
	public GroundTile GetGround(int x, int y){
		for (int z = _sizeZ - 1; z >= 0; --z) {
			List<IPlaceable> contents = GetContent (new Position3D (x, y, z));
			foreach (IPlaceable placeable in contents) {
				if (placeable is GroundTile)
					return (GroundTile)placeable;
			}
		}
		return null;
	}

	public bool Place(IPlaceable placeable, int x, int y, int z){
		return Place(placeable, new Position3D(x, y, z));
	}

	public bool Place(IPlaceable placeable, Position position){
		Position3D position3D = position.AsPosition3D ();

		if (!CanPlace (placeable, position, position3D))
			return false;
		bool addedSuccessfully = true;
		VoxelMap2DLayer layer = _layers [position3D.GetZ()];
		if(layer==null)
			layer = new VoxelMap2DLayer(_sizeX, _sizeY);
		addedSuccessfully=addedSuccessfully&&layer.Place (placeable, (Position)position3D);//TODO this is sligthly inefficient because the check happens at both the 3D map and the 2D slice...
		System.Diagnostics.Debug.Assert (addedSuccessfully, "3D map and 2D slice should match in terms of whether or not this can be added");
		return addedSuccessfully;
	}

	public bool CanPlace(IPlaceable placeable, Position position){
		Position3D position3D = position.AsPosition3D ();
		if (!CanPlace (placeable, position, position3D))
			return false;
		return true;
	}

	private bool CanPlace(IPlaceable placeable, Position position, Position3D position3D){
		if (position3D == null)
			return false;

		if (!placeable.CanPlace (position, this))
			return false;

		List<IPlaceable> overlappingContent = GetContent (position);
		if (overlappingContent == null)
			return false;
		foreach (IPlaceable currentOccupant in overlappingContent) {
			if (!placeable.CanColocate (placeable))
				return false;
		}
		return true;
	}

	public List<IPlaceable> GetContent(){
		List<IPlaceable> content = new List<IPlaceable> ();
		for (int k = 0; k < _sizeZ; ++k) {
			for (int i = 0; i < _sizeX; ++i) {
				for (int j = 0; j < _sizeY; ++j) {
					content.AddRange (GetContent (new Position3D (i, j, k)));
				}
			}
		}
		return content;
	}

	//TODO maybe replace this with an arbitrary query instead of searching just by position
	//for example we might search for a list of all sources of danger within some distance
	public List<IPlaceable> GetContent (Position position){
		Position3D position3D = GetPosition3D (position);
		if (position3D == null) {
			Debug.Log ("not a position3d");
			return null;
		}
		VoxelMap2DLayer layer = _layers[position3D.GetZ()];
		return layer == null ? new List<IPlaceable> () : layer.GetContent(position);
	}

	public Position GetPosition (IPlaceable placeable){
		return null;
	}

	public bool Remove(IPlaceable placeable){
		return true;
	}

	/// <summary>
	/// Returns null if the position is not valid
	/// </summary>
	private Position3D GetPosition3D(Position position){
		Position3D position3D = position.AsPosition3D ();
		if (position3D == null) {
			Debug.Log ("The position must be a position3D "+position);
			return null;
		}
		int x = position3D.GetX ();
		int y = position3D.GetY ();
		int z = position3D.GetZ ();
		if (x >= _sizeX || y >= _sizeY || z >= _sizeZ) {
			Debug.Log ("The specified position is out of bounds ("+x+","+y+","+z+") is outside bounds ("+_sizeX+","+_sizeY+","+_sizeZ+")");
			return null;
		}
		return position3D;
	}

	private bool IsWithinBounds(Position3D position){
		int x = position.GetX ();
		int y = position.GetY ();
		int z = position.GetZ ();
		if (x >= _sizeX || y >= _sizeY || z >= _sizeZ)
			return false;
		return true;
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

		public bool Place(IPlaceable placeable, int x, int y){
			return Place(placeable, new Position2D(x, y));
		}

		public bool Place(IPlaceable placeable, Position position){
			Position2D position2D = position.AsPosition2D ();

			if (!CanPlace (placeable, position, position2D))
				return false;
			
			List<IPlaceable> contents = _content [position2D.GetX (), position2D.GetY ()];
			if (contents == null) {
				contents = new List<IPlaceable>();
				_content [position2D.GetX (), position2D.GetY ()] = contents;
			}
			contents.Add (placeable);
			placeable.Place (position);
			return true;
		}

		public bool CanPlace(IPlaceable placeable, Position position){
			Position2D position2D = position.AsPosition2D();
			if (!CanPlace (placeable, position, position2D))
				return false;
			return true;
		}

		private bool CanPlace(IPlaceable placeable, Position position, Position2D position2D){
			if (position2D == null)
				return false;
			
			if (!placeable.CanPlace (position, this))
				return false;

			List<IPlaceable> overlappingContent = GetContent (position);
			if (overlappingContent == null)
				return false;

			foreach (IPlaceable currentOccupant in overlappingContent) {
				if (!placeable.CanColocate (placeable))
					return false;
			}

			return true;
		}

		public List<IPlaceable> GetContent(){
			List<IPlaceable> content = new List<IPlaceable> ();
			for (int i = 0; i < _sizeX; ++i) {
				for (int j = 0; j < _sizeY; ++j) {
					content.AddRange (GetContent (new Position2D (i, j)));
				}
			}
			return content;
		}

		//TODO maybe replace this with an arbitrary query instead of searching just by position
		//for example we might search for a list of all sources of danger within some distance
		public List<IPlaceable> GetContent (Position position){
			Position2D position2D = GetPosition2D (position);
			if (position2D == null) {
				Debug.Log ("2D pos is null");	
				return null;
			}
			List<IPlaceable> content = _content [position2D.GetX(),position2D.GetY()];
			return content == null ? new List<IPlaceable> () : content;
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

		/// <summary>
		/// Returns null if the position is not valid
		/// </summary>
		private Position2D GetPosition2D(Position position){
			Position2D position2D = position.AsPosition2D ();
			if (position2D == null) {
				Debug.Log ("is not a 2D position");
				return null;
			}
			int x = position2D.GetX ();
			int y = position2D.GetY ();
			if (x >= _sizeX || y >= _sizeY)
				return null;
			return position2D;
		}

		private bool IsWithinBounds(Position2D position){
			int x = position.GetX ();
			int y = position.GetY ();
			if (x >= _sizeX || y >= _sizeY)
				return false;
			return true;
			
		}
	}
}
