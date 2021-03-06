﻿using System;
using System.Collections.Generic;
using UnityEngine;


public class WorldRenderer2D : MonoBehaviour, IViewManager{

	/// <summary>
	/// The tile types. The index here corresponds to the int value of the enum.
	/// </summary>
	public List<Transform> tileTypes = new List<Transform>();
	//public Dictionary<GroundTile.GroundTileType, Transform> tileTypes = new Dictionary<GroundTile.GroundTileType, Transform> ();
	private List<Transform> activeTiles = new List<Transform> ();
	private MultiLevelVoxelMap2D _map;
	private List<IPlaceable> thingsToUpdate = new List<IPlaceable>();
	private static Dictionary<IViewable2D, RectTransform> data2View = new Dictionary<IViewable2D, RectTransform>();

	public WorldRenderer2D(){
	}

	public bool InstantiateMap(MultiLevelVoxelMap2D map){
		_map = map;
		foreach (IPlaceable placeable in map.GetContent ()) {
			CreateRepresentation (placeable);
		}
		return true;
	}

	public bool RegisterUpdate(IPlaceable placeable){
		thingsToUpdate.Add (placeable);
		return true;
	}

	public Transform CreateRepresentation(IPlaceable placeable){
		//TODO replace this with good code
		if (!(placeable is GroundTile))
			return null;
		GroundTile.GroundTileType type = ((GroundTile)placeable).GetTileType();

		Position pos = placeable.GetPosition ();
		Position2D pos2D = pos.AsPosition2D ();

		tileTypes [(int)type].transform.position = new Vector3 (pos2D.GetX (), pos2D.GetY (), 0);
		Transform newTile = GameObject.Instantiate (tileTypes [(int)type]);
		activeTiles.Add (newTile);
		return newTile;
	}

	public interface IViewable{
		Transform GetRepresentation();
	}
	//TODO use this inplace of IPlaceable
	public interface IViewable2D : IViewable{
		new RectTransform GetRepresentation();
	}

	public static RectTransform GetView(IViewable2D viewable){
		return data2View [viewable];
	}

	public void AddViewable(Viewable viewable){
	}
}