using System;
using UnityEngine;

public class Viewable : DataComponent
{
	public Transform _parentCoordinateSystem;
	public Transform _coordinateSystem;//TODO switch to using a non-proprietary library for vector and matrix math
	public enum ViewableType{TILEMAP_2D, SPRITE};
	public Viewable(DataEntity entity) : base(entity){
		_coordinateSystem = new RectTransform ();
	}
}
